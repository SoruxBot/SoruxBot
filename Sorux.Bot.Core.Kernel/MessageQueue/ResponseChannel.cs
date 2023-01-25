using System.Threading.Channels;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Kernel.Builder;
using Sorux.Bot.Core.Kernel.Interface;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.Core.Kernel.MessageQueue;

public class ResponseChannel
{
    private Channel<ResponseContext> _channel;
    private ILoggerService _loggerService;
    private BotContext _botContext;

    public ResponseChannel(ILoggerService loggerService, BotContext botContext)
    {
        this._loggerService = loggerService;
        this._botContext = botContext;
        _channel = Channel.CreateUnbounded<ResponseContext>();
    }

    public async Task<ResponseContext?> GetNextMessageRequest()
    {
        return await _channel.Reader.ReadAsync();
    }

    public async void SetNextMsg(ResponseContext value)
    {
        await _channel.Writer.WriteAsync(value);
    }
}