using Microsoft.Extensions.Configuration;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Bot.Core.Kernel.Interface;
using Sorux.Bot.Core.Kernel.Plugins.Models;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.Core.Kernel.Plugins;

/// <summary>
/// 用于解析插件 Action的参数包，将规范委托转换为特定委托实现参数注入
/// </summary>
public class PluginsCommandLexer
{
    private ILoggerService _loggerService;
    private IPluginsStorage _pluginsStorage;
    private bool _isPermissionEnable;
    private PluginsPermissionDispatcher _pluginsPermissionDispatcher;

    public PluginsCommandLexer(ILoggerService loggerService, IPluginsStorage pluginsStorage,
        IConfiguration configuration, PluginsPermissionDispatcher pluginsPermissionDispatcher)
    {
        this._loggerService = loggerService;
        this._pluginsStorage = pluginsStorage;
        this._isPermissionEnable = configuration.GetRequiredSection("PermissionSystem")["State"]!
            .Equals("Enable");
        this._pluginsPermissionDispatcher = pluginsPermissionDispatcher;
    }

    public PluginFucFlag PluginAction(MessageContext context, PluginsActionDescriptor descriptor)
    {
        //Permission Filter
        if (_isPermissionEnable && !_pluginsPermissionDispatcher.IsContinue(context, descriptor))
            return context.Message!.MsgState;
        string rawMessage = context.Message.GetRawMessage();
        string[] paras = rawMessage.Split(" ");
        List<object> objects = new List<object>();
        objects.Add(context);
        int parasCount = 1;
        bool isValid = true;
        PluginsActionParameter? first = descriptor.ActionParameters.Skip(1).FirstOrDefault();
        if (descriptor.IsParameterLexerDisable) //通用匹配
        {
            objects.Add(rawMessage);
        }
        else
        {
            descriptor.ActionParameters.Skip(1).ToList().ForEach(sp =>
            {
                if (parasCount < paras.Length)
                {
                    //可选的情况 Action(string,int?) 传入 /xxx Ha
                    //二次绑定
                    context.CommandParas![sp.Name] = paras[parasCount];
                    if (sp.ParameterType == typeof(string))
                    {
                        objects.Add(paras[parasCount]);
                    }
                    else if (sp.ParameterType == typeof(int) || sp.ParameterType == typeof(int?))
                    {
                        if (int.TryParse(paras[parasCount], out int result))
                        {
                            objects.Add(result);
                        }
                        else
                        {
                            _loggerService.Warn("PluginsCommandLexer", "Binding Parameter Error! " +
                                                                       "PluginsName:" + descriptor.InstanceTypeName
                                                                       + "Try to parse" + paras[parasCount] +
                                                                       " to int");
                        }
                    }

                    parasCount++;
                }
                else
                {
                    if (sp.IsOptional == false) isValid = false; //参数匹配失败

                    objects.Add(null);
                }
                //转换不了
                //else if (sp.ParameterType == typeof(bool))
                //{}
            });
        }

        if (!isValid)
            return context.Message.MsgState;
        return (PluginFucFlag)descriptor.ActionDelegate.DynamicInvoke(objects.ToArray())!;
    }
}