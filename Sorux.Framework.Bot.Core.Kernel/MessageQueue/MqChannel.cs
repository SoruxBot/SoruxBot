using System.Threading.Channels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.MessageQueue;

/// <summary>
/// 入栈消息队列异步版本
/// </summary>
public class MqChannel
{
    private Channel<MessageContext> _channel;
    private ILoggerService _loggerService;
    private BotContext _botContext;

    public MqChannel(ILoggerService loggerService, BotContext botContext)
    {
        this._loggerService = loggerService;
        this._botContext = botContext;
        _channel = Channel.CreateUnbounded<MessageContext>();
    }

    public async Task<MessageContext?> GetNextMessageRequest()
    {
        return await _channel.Reader.ReadAsync();
    }

    public async void SetNextMsg(MessageContext value)
    {
        await _channel.Writer.WriteAsync(value);
    }
}