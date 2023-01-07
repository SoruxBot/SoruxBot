using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Plugins;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.APIServices;

public class BasicApi : IBasicAPI
{
    private BotContext _botContext;
    private ILoggerService _loggerService;
    private PluginsHost _pluginsHost;
    private IResponseQueue _responseQueue;
    public BasicApi(BotContext botContext, ILoggerService loggerService, PluginsHost pluginsHost,IResponseQueue responseQueue)
    {
        this._botContext = botContext;
        this._loggerService = loggerService;
        this._pluginsHost = pluginsHost;
        this._responseQueue = responseQueue;
    }
    
    public void SendPrivateMessage(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerId,
            MessageContent = content,
            ResopnseRoute = "sendPrivateMessage"
        };
        
        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendPrivateMessage"
        };
        _responseQueue.SetNextReponse(response);
    }

    public Task<string> SendPrivateMessageAsync(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerId,
            MessageContent = content,
            ResopnseRoute = "sendPrivateMessage"
        };
        
        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendPrivateMessage"
        };
        return _pluginsHost.ActionAysnc(response);
    }
}