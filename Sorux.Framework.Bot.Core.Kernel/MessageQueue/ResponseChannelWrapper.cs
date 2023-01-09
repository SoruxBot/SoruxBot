using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.MessageQueue;

public class ResponseChannelWrapper : IResponseQueue
{
    private ResponseChannel _rpChannel;
    
    public ResponseChannelWrapper(ILoggerService loggerService,BotContext botContext)
    {
        _rpChannel = new ResponseChannel(loggerService,botContext);
    }

    public void SetNextReponse(ResponseContext context)
    {
        _rpChannel.SetNextMsg(context);
    }

    public ResponseContext? GetNextResponse()
    {
        return _rpChannel.GetNextMessageRequest().Result;
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