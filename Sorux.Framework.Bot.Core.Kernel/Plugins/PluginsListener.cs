using System.Text;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins;

public class PluginsListener
{
    private BotContext _botContext;
    private ILoggerService _loggerService;
    public PluginsListener(BotContext botContext,ILoggerService loggerService)
    {
        this._botContext = botContext;
        this._loggerService = loggerService;
    }

    private List<PluginsListenerDescriptor> _map = new List<PluginsListenerDescriptor>();

    private bool MatchRoute(PluginsListenerDescriptor descriptor, MessageContext context)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(descriptor.eventType.ToString());
        if (descriptor.targetPlatform != null)
        {
            stringBuilder.Append(";" + descriptor.targetPlatform);
            if (descriptor.targetAction != null)
            {
                stringBuilder.Append(";" + descriptor.targetAction);
            }
        }

        if (context.ActionRoute.Contains(stringBuilder.ToString()))
            return true;
        return false;
    }
    
    /// <summary>
    /// 进入Filter队列，并且判断是否需要继续执行 Dispatcher
    /// </summary>
    /// <param name="context"></param>
    /// <param name="newContext"></param>
    /// <returns></returns>
    public bool Filter(MessageContext context,out MessageContext newContext)
    {
        newContext = context;
        foreach (var descriptor in _map)
        {
            if (MatchRoute(descriptor,context) && descriptor.action(context))
            {
                descriptor.nextContext = context;
                //监听拦截成功
                if (descriptor.isIntercept)
                {
                    return false;//不继续执行
                }else if (newContext.Message != null)
                {
                    newContext.Message.MsgState = descriptor.flag;
                }
            }
        }
        return true;
    }

    public void RemoveListener(PluginsListenerDescriptor pluginsListenerDescriptor)
        => _map.Remove(pluginsListenerDescriptor);

    public void AddListener(PluginsListenerDescriptor pluginsListenerDescriptor)
        => _map.Add(pluginsListenerDescriptor);
}