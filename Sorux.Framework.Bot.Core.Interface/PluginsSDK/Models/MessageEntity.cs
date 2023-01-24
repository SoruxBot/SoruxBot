using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

public class MessageEntity
{
    /// <summary>
    /// 原始消息字符串，包含已经解析的如 Action 等
    /// </summary>
    public string Meta { get; init; }

    /// <summary>
    /// 消息的状态
    /// </summary>
    public PluginFucFlag MsgState { get; set; }

    /// <summary>
    /// 获得 Meta 中的原始消息
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string GetRawMessage() => Meta;
}