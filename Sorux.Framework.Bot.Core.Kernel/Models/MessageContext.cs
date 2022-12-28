using Sorux.Framework.Bot.Core.Kernel.Models;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
/// <summary>
/// MessageContext 类 --- 负责传递对应的消息细节
/// </summary>
public class MessageContext
{
    /// <summary>
    /// 事件的名称
    /// </summary>
    public string Action { get; init; } 
    /// <summary>
    /// 接受触发的机器人账号
    /// </summary>
    public string BotAccount { get; init; } 
    /// <summary>
    /// 触发事件的平台
    /// </summary>
    public string TargetPlatform { get; init; } 
    /// <summary>
    /// 事件类型
    /// </summary>
    public EventType MessageEventType { get; init; } 
    /// <summary>
    /// 触发这个消息的个体Id，如果触发消息的是非个体，那么会使用 AlterTriggerId 的枚举类型
    /// </summary>
    public string TriggerId { get; init; }
    /// <summary>
    /// 来源组信息：
    /// 如果是个体信息，那么这个值等于 TriggerId;
    /// 如果是群聊信息，那么这个值等于群组的账号;
    /// 如果是频道信息，那么这个值等于频道的主账号;
    /// </summary>
    public string TriggerPlatformId { get; init; }
    /// <summary>
    /// 预留的辅助 Id，具体语境根据 TriggerPlatformId 有具体的定义，例如其为频道的主账号时，那么 TiedId 表示的时频道的子频道账号
    /// </summary>
    public string TiedId { get; init; }
    /// <summary>
    /// 长对话支持模块，可以不存在
    /// </summary>
    public LongMessageContext? LongMessageContext { get; init; }
    /// <summary>
    /// 消息实体，使用本对象应该遵循语义为主的处理方式
    /// </summary>
    public MessageEntity Message { get; init; }
    /// <summary>
    /// 原始命令参数列表，本列表会存储任何原始的参数信息（不含有母命名头）。
    /// 请仅在无法通过参数注入的情况下使用本命令。
    /// 参数的 Key 为特性注入时提供的参数 Key
    /// </summary>
    public Dictionary<string,string?> CommandParas { get; init; }
}