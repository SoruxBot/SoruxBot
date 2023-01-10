using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Plugins;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.APIServices;

public class LongMessageCommunicate : ILongMessageCommunicate
{

    private IBot _bot;
    private ILoggerService _loggerService;
    private int _globalTimeOut;
    public LongMessageCommunicate(IBot _bot, ILoggerService loggerService)
    {
        this._bot = _bot;
        this._loggerService = loggerService;
        this._globalTimeOut = int
            .Parse(_bot.Configuration.GetRequiredSection("LongCommunicateFunction")["DefaultTimeOutLimit"]!);
    }
    
    public Task<MessageContext?> ReadNextPrivateMessageAsync(MessageContext context,int? timeOut)
    {
        return CreateGenericListenerAsync(context.MessageEventType,
            context.TargetPlatform,
            context.ActionRoute.Split(";")[2],
            sp => sp.TriggerId == context.TriggerId,
            true,
            PluginFucFlag.MsgIntercepted,
            timeOut);
    }

    public Task<MessageContext?> ReadNextGroupMessageAsync(LongCommunicateType type, MessageContext context,int? timeOut)
    {
        return CreateGenericListenerAsync(context.MessageEventType,
            context.TargetPlatform,
            context.ActionRoute.Split(";")[2],
            sp => sp.TriggerId == context.TriggerId && sp.TriggerPlatformId == context.TriggerPlatformId,
            true,
            PluginFucFlag.MsgIntercepted,
            timeOut);
    }
    

    public async Task<MessageContext?> CreateGenericListenerAsync(EventType eventType, string? targetPlatform,
        string? targetAction, Func<MessageContext, bool> action, bool isIntercept, PluginFucFlag flag,int? timeOut)
    {
        if (timeOut == null)
            timeOut = _globalTimeOut;
        PluginsListenerDescriptor pluginsListenerDescriptor = new()
        {
            eventType = eventType,
            targetPlatform = targetPlatform,
            targetAction = targetAction,
            action = action,
            isIntercept = isIntercept,
            flag = flag,
            nextContext = null
        };
        
        _bot.Context.ServiceProvider.GetRequiredService<PluginsListener>().AddListener(pluginsListenerDescriptor);

        var tcs = new TaskCompletionSource<bool>();
        MessageContext res = null;
        using (var cts = new CancellationTokenSource(timeOut.Value * 1000)) 
        {
            Task<MessageContext> task = Task.Run(() =>
            {
                while (true)
                {
                    if (pluginsListenerDescriptor.nextContext != null)
                    {
                        return pluginsListenerDescriptor.nextContext;
                    }
                    Thread.Sleep(10);
                }
            });
            using (cts.Token.Register(() => tcs.TrySetResult(true)))
            {
                if (task == await (Task.WhenAny(task, tcs.Task)))
                {
                     res = await task;
                }
                else
                {
                    _loggerService.Warn("LongCommunicateListener"
                        ,"Listener Timeout...Stop it:");
                }
            }
        }
        _bot.Context.ServiceProvider.GetRequiredService<PluginsListener>().RemoveListener(pluginsListenerDescriptor);
        return res;
    }
}