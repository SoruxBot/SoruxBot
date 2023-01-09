using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.MessageQueue;

public class MqChannelWrapper : IMessageQueue
{
    private MqChannel _mqChannel;
    
    public MqChannelWrapper(ILoggerService loggerService,BotContext botContext)
    {
        _mqChannel = new MqChannel(loggerService,botContext);
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
        throw new NotImplementedException();
    }

    public void SaveIntoLocalStorage()
    {
        throw new NotImplementedException();
    }

    public void DisposeFromLocalStorage()
    {
        throw new NotImplementedException();
    }
}