using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Kernel.Builder;
using Sorux.Bot.Core.Kernel.Interface;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.Core.Kernel.MessageQueue;

public class MqChannelWrapper : IMessageQueue
{
    private MqChannel _mqChannel;
    private ILoggerService _loggerService;

    public MqChannelWrapper(ILoggerService loggerService, BotContext botContext)
    {
        _mqChannel = new MqChannel(loggerService, botContext);
        this._loggerService = loggerService;
    }

    public MessageContext? GetNextMessageRequest()
    {
        return _mqChannel.GetNextMessageRequest().Result;
    }

    public void SetNextMsg(MessageContext value)
    {
        _mqChannel.SetNextMsg(value);
    }

    public void RestoreFromLocalStorage()
    {
        _loggerService.Error("MessageChannelWrapper", "This version of Message Queue didn't implement LocalStorage");
    }

    public void SaveIntoLocalStorage()
    {
        _loggerService.Error("MessageChannelWrapper", "This version of Message Queue didn't implement LocalStorage");
    }

    public void DisposeFromLocalStorage()
    {
        _loggerService.Error("MessageChannelWrapper", "This version of Message Queue didn't implement LocalStorage");
    }
}