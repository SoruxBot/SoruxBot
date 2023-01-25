using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sorux.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Bot.Core.Kernel.Builder;
using Sorux.Bot.Core.Kernel.DataStorage;
using Sorux.Bot.Core.Kernel.Interface;
using Sorux.Bot.Core.Kernel.Plugins.Models;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.Core.Kernel.Plugins;

/// <summary>
/// 插件调度器，负责分配事件到具体的插件。
/// </summary>
public class PluginsDispatcher
{
    private BotContext _botContext;
    private ILoggerService _loggerService;
    private IPluginsStorage _pluginsStorage;
    private string _globalCommandPrefix;
    private PluginsListener _pluginsListener;
    private bool _isLongCommunicateEnable;
    private PermissionStorage _permissionStorage;

    public PluginsDispatcher(BotContext botContext, ILoggerService loggerService, IPluginsStorage pluginsStorage,
        IConfiguration configuration, PluginsListener pluginsListener, PermissionStorage permissionStorage)
    {
        this._botContext = botContext;
        this._loggerService = loggerService;
        this._pluginsStorage = pluginsStorage;
        this._pluginsListener = pluginsListener;
        IConfigurationSection section = configuration.GetRequiredSection("CommunicateTrigger");
        this._globalCommandPrefix = section["State"]!.Equals("Enable") ? section["TriggerChar"]! : "";
        this._isLongCommunicateEnable = configuration.GetRequiredSection("LongCommunicateFunction")["State"]!
            .Equals("Enable");
        this._permissionStorage = permissionStorage;
    }

    public delegate PluginFucFlag ActionDelegate(object instance, params object[] args);

    //插件按照触发条件可以分为选项式命令触发和事件触发
    //前者针对某个特定 EventType 的某个特定的语句触发某个特定的方法
    //后者针对某个通用的 EventType 进行触发
    private Dictionary<string, List<PluginsActionDescriptor>> _matchList = new();


    /// <summary>
    /// 注册指令路由
    /// </summary>
    /// <param name="filepath"></param>
    /// <param name="name"></param>
    public void RegisterCommandRoute(string filepath, string name)
    {
        Assembly assembly = Assembly.LoadFile(filepath);
        Type[] types = assembly.GetExportedTypes();
        Dictionary<string, PermissionNode> matchPermissionNodes = new Dictionary<string, PermissionNode>();
        //注册权限系统
        PluginsPermissionList? pluginsPermissionList = null;
        try
        {
            var path = Path.Join(DsLocalStorage.GetPluginsConfigDirectory(),
                name.Replace(".dll", ".json"));
            pluginsPermissionList = JsonConvert.DeserializeObject<PluginsPermissionList>(
                File.ReadAllText(path));
        }
        catch (Exception)
        {
            _loggerService.Error("PluginsRegister", "The plugin:" + name + " loses json file:" +
                                                    name.Replace(".dll", ".json") +
                                                    "ErrorCode:EX0003");
            return;
        }

        if (pluginsPermissionList != null && pluginsPermissionList.PermissionNode != null)
        {
            pluginsPermissionList.PermissionNode.ForEach(sp => { matchPermissionNodes.Add(sp.Node, sp); });
        }

        //注册默认权限
        if (pluginsPermissionList != null && pluginsPermissionList.PermissionDefaultConfig != null)
        {
            pluginsPermissionList.PermissionDefaultConfig.ForEach(sp =>
            {
                if (!_permissionStorage.GetNodeCondition(sp.Node + "SoruxBot" + sp.Platform + "SoruxBot"
                                                         + matchPermissionNodes[sp.Node].ConditionChar + sp.Condition))
                {
                    _permissionStorage
                        .AddPermission(
                            sp.Platform + "SoruxBot" + matchPermissionNodes[sp.Node].ConditionChar + sp.Condition,
                            sp.Node,
                            sp.Node + "SoruxBot" + sp.Platform + "SoruxBot"
                            + matchPermissionNodes[sp.Node].ConditionChar + sp.Condition);
                }
            });
        }

        foreach (var className in types)
        {
            if (className.BaseType == typeof(BotController))
            {
                _loggerService.Debug("CommandRoute", "Controller is caught! For type ->" + className.Name);
                //缓存 Controller
                ConstructorInfo constructorInfo = className.GetConstructors()[0];
                ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
                List<object> objects = new List<object>();
                IServiceProvider serviceProvider = _botContext.ServiceProvider;
                foreach (var parameterInfo in parameterInfos)
                {
                    #region 匹配参数

                    if (parameterInfo.ParameterType == typeof(BotContext))
                    {
                        objects.Add(_botContext);
                    }
                    else if (parameterInfo.ParameterType == typeof(ILoggerService))
                    {
                        objects.Add(_loggerService);
                    }
                    else
                    {
                        objects.Add(serviceProvider.GetRequiredService(parameterInfo.ParameterType));
                    }
                    #endregion
                }

                _pluginsStorage.SetPluginInstance(name + "." + className.Name,
                    Activator.CreateInstance(className, objects.ToArray())!);

                MethodInfo[] methods = className.GetMethods();
                foreach (var methodInfo in methods)
                {
                    if (!methodInfo.IsDefined(typeof(EventAttribute))) continue;
                    
                    var methodEventAttribute = methodInfo.GetCustomAttribute<EventAttribute>();
                    if (methodEventAttribute is null)
                    {
                        _loggerService.Warn("PluginsDispatcher",
                            "Missing Type for EventAttribute , plugin:" + name);
                        throw new Exception("Missing Type for EventAttribute, plugin:" + name);
                    }

                    string commandTriggerType = methodEventAttribute!.EventType.ToString();
                    var methodEventCommand = methodInfo.GetCustomAttribute<CommandAttribute>();
                    if (methodEventCommand is null)
                    {
                        _loggerService.Warn("PluginsDispatcher",
                            "Missing Type for CommandAttribute , plugin:" + name);
                        throw new Exception("Missing Type for CommandAttribute, plugin:" + name);
                    }

                    //判断是否持有平台特定的特性
                    if (methodInfo.IsDefined(typeof(PlatformConstraintAttribute)))
                    {
                        var methodPlatformConstraint = methodInfo.GetCustomAttribute<PlatformConstraintAttribute>();
                        commandTriggerType =
                            commandTriggerType + ";" + methodPlatformConstraint!.PlatformConstraint;
                    }

                    PluginsActionDescriptor pluginsActionDescriptor = new();
                    //权限模块设置
                    if (methodInfo.IsDefined(typeof(PermissionAttribute)))
                    {
                        var permissionAttribute = methodInfo.GetCustomAttribute<PermissionAttribute>();
                        var permissionNode = matchPermissionNodes[permissionAttribute!.PermissionNode];
                        var pluginsPermissionDescriptor = new PluginsPermissionDescriptor();
                        pluginsPermissionDescriptor.PermissionNode = permissionAttribute.PermissionNode;
                        pluginsPermissionDescriptor.Description = permissionNode.Description;
                        if (permissionNode.ConditionType.Equals("BasicModel"))
                        {
                            switch (permissionNode.ConditionChar)
                            {
                                case "TriggerId":
                                    pluginsPermissionDescriptor.FilterAction =
                                        context => "TriggerId" + context.TriggerId;
                                    pluginsActionDescriptor.PluginsPermissionDescriptor =
                                        pluginsPermissionDescriptor;
                                    break;
                                case "TriggerPlatformId":
                                    pluginsPermissionDescriptor.FilterAction =
                                        context => "TriggerPlatformId" + context.TriggerPlatformId;
                                    pluginsActionDescriptor.PluginsPermissionDescriptor =
                                        pluginsPermissionDescriptor;
                                    break;
                                default:
                                    _loggerService.Warn("PluginsPermission", "Plugins:" + name +
                                                                             "'s jsonFile use unsupported permissionConditionChar");
                                    return;
                            }
                        }
                        else
                        {
                            pluginsPermissionDescriptor.FilterAction =
                                context => permissionNode.ConditionChar
                                           + context.UnderProperty[permissionNode.ConditionChar];
                            pluginsActionDescriptor.PluginsPermissionDescriptor =
                                pluginsPermissionDescriptor;
                        }
                    }

                    string commandPrefix = methodEventCommand.CommandPrefix switch
                    {
                        CommandAttribute.Prefix.None => "",
                        CommandAttribute.Prefix.Single => _pluginsStorage.GetPluginInfor(name,
                            "CommandPrefixContent"),
                        CommandAttribute.Prefix.Global => _globalCommandPrefix,
                        _ => ""
                    };
                    //生成 Controller 的委托
                    ParameterInfo[] parameters = methodInfo.GetParameters();

                    if (methodEventCommand!.Command[0].Equals("[SF-ALL]"))
                    {
                        pluginsActionDescriptor.IsParameterLexerDisable = true;
                        var args = new List<Type>(methodInfo.GetParameters().Select(sp => sp.ParameterType));
                        Type delegateType;
                        args.Add(methodInfo.ReturnType);
                        delegateType = Expression.GetFuncType(args.ToArray());
                        pluginsActionDescriptor.ActionDelegate =
                            methodInfo.CreateDelegate(delegateType,
                                _pluginsStorage.GetPluginInstance(name + "." + className.Name));
                    }
                    else
                    {
                        string[] paras = methodEventCommand!.Command[0].Split(" ").Skip(1).ToArray();
                        int count = 0;

                        //添加必然存在的参数 MessageContext
                        PluginsActionParameter messageContextPara = new PluginsActionParameter();
                        messageContextPara.IsOptional = false;
                        messageContextPara.Name = "context";
                        messageContextPara.ParameterType = typeof(MessageContext);
                        pluginsActionDescriptor.ActionParameters.Add(messageContextPara);

                        foreach (var parameterInfo in parameters.Skip(1))
                        {
                            //默认插件作者提供的命令列表的参数顺序和 Action 的函数顺序一致，否者绑定失败需要作者自己从 Context获取
                            PluginsActionParameter pluginsActionParameter = new PluginsActionParameter();
                            pluginsActionParameter.IsOptional = paras[count].Substring(0, 1).Equals("<");
                            pluginsActionParameter.Name =
                                paras[count].Substring(1, paras[count].Length - 2); //[message] 故全长-2
                            pluginsActionParameter.ParameterType = parameterInfo.ParameterType;
                            pluginsActionDescriptor.ActionParameters.Add(pluginsActionParameter);
                            count++;
                        }

                        pluginsActionDescriptor.IsParameterBinded = true; //绑定成功 [其实绑定失败了就无法 Invoke了]
                        var args = new List<Type>(methodInfo.GetParameters().Select(sp => sp.ParameterType));
                        Type delegateType;
                        args.Add(methodInfo.ReturnType);
                        delegateType = Expression.GetFuncType(args.ToArray());
                        pluginsActionDescriptor.ActionDelegate =
                            methodInfo.CreateDelegate(delegateType,
                                _pluginsStorage.GetPluginInstance(name + "." + className.Name));
                    }

                    #region EMIT

                    //List<Type> methodParaAll = new List<Type>();
                    //methodParaAll.Add(className);
                    //methodParaAll.AddRange(pluginsActionDescriptor
                    //             .ActionParameters.Select( s => s.ParameterType));
                    //List<Type> objparas = new List<Type>();
                    //methodParaAll.ToList().ForEach( _ => objparas.Add(typeof(object)));
                    //var method = new DynamicMethod(methodInfo.Name,typeof(PluginFucFlag),
                    //    new Type[]{typeof(object),typeof(object[])});
                    //var il = method.GetILGenerator();
                    //il.Emit(OpCodes.Ldarg,0);
                    //il.Emit(OpCodes.Ldarg,1);
                    //int paraCount = 0;
                    //pluginsActionDescriptor.ActionParameters.ForEach(sp =>
                    //{
                    //    il.Emit(OpCodes.Ldc_I4,paraCount);
                    //    il.Emit(OpCodes.Ldelem_Ref);
                    //    if (sp.ParameterType.IsValueType)
                    //    {
                    //        il.Emit(OpCodes.Unbox_Any, sp.ParameterType);
                    //    }
                    //});
                    //il.Emit(OpCodes.Call,methodInfo);
                    //il.Emit(OpCodes.Ret);
                    //pluginsActionDescriptor.ActionDelegate = method.CreateDelegate(typeof(ActionDelegate));

                    #endregion

                    pluginsActionDescriptor.InstanceTypeName = name + "." + className.Name;
                    //特判匹配
                    if (pluginsActionDescriptor.IsParameterLexerDisable)
                    {
                        if (_matchList.TryGetValue(commandTriggerType + "/[SF-ALL]",
                                out List<PluginsActionDescriptor>? list))
                        {
                            list.Add(pluginsActionDescriptor);
                        }
                        else
                        {
                            list = new List<PluginsActionDescriptor>();
                            list.Add(pluginsActionDescriptor);
                            _matchList.Add(commandTriggerType + "/[SF-ALL]",
                                list);
                        }
                    }
                    else
                    {
                        foreach (var s in methodEventCommand.Command)
                        {
                            //添加进入路由
                            //[Type];[Platform];[Action]/<Prefix>[Command]
                            //Command这个地方用 Delegate 来记录
                            if (_matchList.TryGetValue(commandTriggerType + "/" + commandPrefix + s.Split(" ")[0],
                                    out List<PluginsActionDescriptor>? list))
                            {
                                list.Add(pluginsActionDescriptor);
                            }
                            else
                            {
                                list = new List<PluginsActionDescriptor>();
                                list.Add(pluginsActionDescriptor);
                                _matchList.Add(commandTriggerType + "/" + commandPrefix + s.Split(" ")[0],
                                    list);
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 得到路由被注册后的委托方法
    /// </summary>
    /// <returns></returns>
    public List<PluginsActionDescriptor>? GetAction(string route, ref MessageContext messageContext)
    {
        //Action捕获前，判断是否是长对话
        if (_isLongCommunicateEnable && !_pluginsListener.Filter(messageContext, out messageContext))
            return null;
        //ActionGet
        var list = new List<PluginsActionDescriptor>();
        string[] parts = route.Split("/");
        string[] waittingList = parts[0].Split(";");
        switch (waittingList.Length)
        {
            case 1: //通用匹配
                if (_matchList.TryGetValue(waittingList[0] + "/[SF-ALL]", out var tempAA))
                {
                    list.AddRange(tempAA);
                }

                if (_matchList.TryGetValue(waittingList[0] + "/" + parts[1], out list))
                    return list;
                return null;
            case 2: //平台特定的匹配，自然包含了通用匹配
                if (_matchList.TryGetValue(waittingList[0] + "/[SF-ALL]", out var tempBB))
                {
                    list.AddRange(tempBB);
                }

                if (_matchList.TryGetValue(waittingList[0] + ";" + waittingList[1] + "/" + parts[1], out var tempA))
                {
                    list.AddRange(tempA);
                }
                else if (_matchList.TryGetValue(waittingList[0] + "/" + parts[1], out var tempB))
                {
                    list.AddRange(tempB);
                }

                return list.Count != 0 ? list : null;
            case 3: //平台及平台方法特定的匹配
                if (_matchList.TryGetValue(waittingList[0] + "/[SF-ALL]", out var tempCC))
                {
                    list.AddRange(tempCC);
                }

                if (_matchList.TryGetValue(waittingList[0] + ";" +
                                           waittingList[1] + ";" + waittingList[2] + "/" +
                                           parts[1], out var tempC))
                {
                    list.AddRange(tempC);
                }
                else if (_matchList.TryGetValue(waittingList[0] + ";" + waittingList[1] + "/" + parts[1],
                             out var tempD))
                {
                    list.AddRange(tempD);
                }
                else if (_matchList.TryGetValue(waittingList[0] + "/" + parts[1], out var tempE))
                {
                    list.AddRange(tempE);
                }

                return list.Count != 0 ? list : null;
            default:
                throw new Exception("Error Route");
        }
    }
}