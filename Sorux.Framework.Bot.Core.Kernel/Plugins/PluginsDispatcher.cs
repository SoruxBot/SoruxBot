using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins;
/// <summary>
/// 插件调度器，负责分配事件到具体的插件。
/// </summary>
public class PluginsDispatcher
{
    private BotContext _botContext;
    private ILoggerService _loggerService;
    private IPluginsStorage _pluginsStorage;
    private string _globalCommandPrefix;

    public PluginsDispatcher(BotContext botContext,ILoggerService loggerService,IPluginsStorage pluginsStorage,IConfiguration configuration)
    {
        this._botContext = botContext;
        this._loggerService = loggerService;
        this._pluginsStorage = pluginsStorage;
        IConfigurationSection section = configuration.GetRequiredSection("CommunicateTrigger");
        this._globalCommandPrefix = section["State"]!.Equals("True") ? section["TriggerChar"]! : "";
    }
    
    //插件按照触发条件可以分为选项式命令触发和事件触发
    //前者针对某个特定 EventType 的某个特定的语句触发某个特定的方法
    //后者针对某个通用的 EventType 进行触发
    private Dictionary<string, PluginsActionDescriptor> _matchList = new ();
    /// <summary>
    /// 注册指令路由
    /// </summary>
    /// <param name="filepath"></param>
    /// <param name="name"></param>
    public void RegisterCommandRoute(string filepath,string name)
    {
        Assembly assembly = Assembly.LoadFile(filepath);
        Type[] types = assembly.GetExportedTypes();
        foreach (var className in types)
        {
            if (className.BaseType == typeof(BotController))
            { 
                _loggerService.Debug("CommandRoute","Controller is caught! For type ->" + className.Name);
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
                    }else if (parameterInfo.ParameterType == typeof(ILoggerService))
                    {
                        objects.Add(_loggerService);
                    }else if (parameterInfo.ParameterType == typeof(IBasicAPI))
                    {
                        objects.Add(serviceProvider.GetRequiredService<IBasicAPI>());
                    }else if (parameterInfo.ParameterType == typeof(ILongMessageCommunicate))
                    {
                        objects.Add(serviceProvider.GetRequiredService<ILongMessageCommunicate>());
                    }else if (parameterInfo.ParameterType == typeof(IPluginsDataStorage))
                    {
                        objects.Add(serviceProvider.GetRequiredService<IPluginsDataStorage>());
                    }else if (parameterInfo.ParameterType == typeof(IPluginsStoragePermanentAble))
                    {
                        objects.Add(serviceProvider.GetRequiredService<IPluginsStoragePermanentAble>());
                    }
                    #endregion
                }
                _pluginsStorage.SetPluginInstance(name+ "." + className.Name, Activator.CreateInstance(className,objects.ToArray())!);
                
                MethodInfo[] methods = className.GetMethods();
                foreach (var methodInfo in methods)
                {
                    if (methodInfo.IsDefined(typeof(EventAttribute)))
                    {
                        var methodEventAttribute = methodInfo.GetCustomAttribute<EventAttribute>();
                        if (methodEventAttribute is null)
                        {
                            _loggerService.Warn("PluginsDispatcher","Missing Type for EventAttribute , plugin:" + name);
                            throw new Exception("Missing Type for EventAttribute, plugin:" + name);
                        }
                        string commandTriggerType = methodEventAttribute!.EventType.ToString();
                        var methodEventCommand = methodInfo.GetCustomAttribute<CommandAttribute>();
                        if (methodEventCommand is null)
                        {
                            _loggerService.Warn("PluginsDispatcher","Missing Type for CommandAttribute , plugin:" + name);
                            throw new Exception("Missing Type for CommandAttribute, plugin:" + name);
                        }
                        //判断是否持有平台特定的特性
                        if (methodInfo.IsDefined(typeof(PlatformConstraintAttribute)))
                        {
                            var methodPlatformConstraint = methodInfo.GetCustomAttribute<PlatformConstraintAttribute>();
                            commandTriggerType = commandTriggerType + ";" + methodPlatformConstraint!.PlatformConstraint;
                        }
                        string commandPrefix = methodEventCommand.CommandPrefix switch
                           {
                               CommandAttribute.Prefix.None   => "",
                               CommandAttribute.Prefix.Single => _pluginsStorage.GetPluginInfor(name,"CommandPrefixContent"),
                               CommandAttribute.Prefix.Global => _globalCommandPrefix,
                               _                              => ""
                           };
                        //生成 Controller 的委托
                        PluginsActionDescriptor pluginsActionDescriptor = new();
                        ParameterInfo[] parameters = methodInfo.GetParameters();
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
                            pluginsActionParameter.Name = paras[count].Substring(1, paras[count].Length - 1);
                            pluginsActionParameter.ParameterType = parameterInfo.ParameterType;
                            pluginsActionDescriptor.ActionParameters.Add(pluginsActionParameter);
                            count++;
                        }
                        pluginsActionDescriptor.IsParameterBinded = true;  //绑定成功 [其实绑定失败了就无法 Invoke了]
                        //e.g. public PluginFucFlag EchoPrivilege(MessageContext context,string msg)
                        List<Type> methodParaAll = new List<Type>();

                        var method = new DynamicMethod(methodInfo.Name, typeof(PluginFucFlag), 
                            pluginsActionDescriptor.ActionParameters.Select(s => s.ParameterType).ToArray());
                        var il = method.GetILGenerator();
                        int emitCount = 1;
                        List<Type> optionalTypes = new List<Type>();
                        pluginsActionDescriptor.ActionParameters.ForEach(sp =>
                        {
                            if (sp.IsOptional)
                            {
                                optionalTypes.Add(sp.ParameterType);
                            }
                            il.Emit(emitCount switch
                                                {
                                                    1 => OpCodes.Ldarg_1,
                                                    2 => OpCodes.Ldarg_2,
                                                    3 => OpCodes.Ldarg_3,
                                                    _ => OpCodes.Ldarg_S
                                                });
                            emitCount++;
                        });
                        il.Emit(OpCodes.Callvirt,methodInfo);

                        //il.EmitCall(OpCodes.Call,methodInfo,optionalTypes.ToArray());
                        il.Emit(OpCodes.Pop);
                        il.Emit(OpCodes.Ret);
                        pluginsActionDescriptor.ActionDelegate = (parameters.Length) switch
                        {
                            1 => method.CreateDelegate(typeof(Func<,>).MakeGenericType(parameters[0].ParameterType,typeof(PluginFucFlag))),
                            2 => method.CreateDelegate(typeof(Func<,,>).MakeGenericType(parameters[0].ParameterType,
                                parameters[1].ParameterType),typeof(PluginFucFlag)),
                            3 => method.CreateDelegate(typeof(Func<,,,>).MakeGenericType(parameters[0].ParameterType,
                                parameters[1].ParameterType, parameters[2].ParameterType,typeof(PluginFucFlag))),
                            4 => method.CreateDelegate(typeof(Func<,,,,>).MakeGenericType(parameters[0].ParameterType,
                                parameters[1].ParameterType, parameters[2].ParameterType,parameters[3].ParameterType,typeof(PluginFucFlag))),
                            _ => throw new Exception("Cannot match type")
                        };
                        //Test Var
                        MessageContext messageContext = new MessageContext(
                            "action","botaccount","qq",
                            EventType.SoloMessage,"172","123",
                            "123",new MessageEntity()
                            );
                        pluginsActionDescriptor.ActionDelegate.DynamicInvoke(messageContext);
                        
                        foreach (var s in methodEventCommand!.Command) //Command(Prefix,"123")
                        {
                            //添加进入路由
                            //[Type];[Platform];[Action]/<Prefix>[Command]
                            //Command这个地方用 Delegate 来记录
                            _matchList.Add(commandTriggerType + "/" + commandPrefix + s.Split(" ")[0],
                                pluginsActionDescriptor);
                        }
                    }
                }
            }
        }
    }
}