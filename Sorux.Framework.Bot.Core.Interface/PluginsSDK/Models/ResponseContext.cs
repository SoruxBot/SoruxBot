namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

public class ResponseContext
{
    /// <summary>
    /// 表示 Response 所携带的 Message 对象
    /// </summary>
    public MessageContext Message { get; init; }
    
    /// <summary>
    /// 表示 Response 需要执行的对应的 ResponseAction.
    /// </summary>
    public String ResponseAction { get; init; }
}