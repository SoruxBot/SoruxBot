using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.MessageQueue;

public class ResponseChannelWrapper : IResponseQueue
{
    private ResponseChannel _rpChannel;
    private ILoggerService _loggerService;

    public ResponseChannelWrapper(ILoggerService loggerService, BotContext botContext)
    {
        _rpChannel = new ResponseChannel(loggerService, botContext);
        this._loggerService = loggerService;
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
        _loggerService.Error("ResponseChannelWrapper", "This version of Response Queue didn't implement LocalStorage");
    }

    public void SaveIntoLocalStorage()
    {
        _loggerService.Error("ResponseChannelWrapper", "This version of Response Queue didn't implement LocalStorage");
    }

    public void DisposeFromLocalStorage()
    {
        _loggerService.Error("ResponseChannelWrapper", "This version of Response Queue didn't implement LocalStorage");
    }
}