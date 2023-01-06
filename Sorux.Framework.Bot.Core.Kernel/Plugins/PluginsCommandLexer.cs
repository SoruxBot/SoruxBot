using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins;

/// <summary>
/// 用于解析插件 Action的参数包，将规范委托转换为特定委托实现参数注入
/// </summary>
public class PluginsCommandLexer
{

    private ILoggerService _loggerService;
    private IPluginsStorage _pluginsStorage;
    public PluginsCommandLexer(ILoggerService loggerService,IPluginsStorage pluginsStorage)
    {
        this._loggerService = loggerService;
        this._pluginsStorage = pluginsStorage;
    }
    
    public PluginFucFlag PluginAction(MessageContext context, PluginsActionDescriptor descriptor)
    {
        string rawMessage = context.Message.GetRawMessage();
        string[] paras = rawMessage.Split(" ");
        List<object> objects = new List<object>();
        objects.Add(context);
        int parasCount = 1;
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
                                                                       + "Try to parse" + paras[parasCount] + " to int");
                        }

                    }
                    parasCount++;
                }
                else
                {
                    objects.Add(null);
                }
                //转换不了
                //else if (sp.ParameterType == typeof(bool))
                //{}
            });

        return (PluginFucFlag)descriptor.ActionDelegate.DynamicInvoke(objects.ToArray())!;
    }
}