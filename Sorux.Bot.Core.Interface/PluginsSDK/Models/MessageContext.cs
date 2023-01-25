﻿namespace Sorux.Bot.Core.Interface.PluginsSDK.Models;

/// <summary>
/// MessageContext 类 --- 负责传递对应的消息细节
/// </summary>
public class MessageContext
{
    /// <summary>
    /// 事件的路由
    /// </summary>
    public string ActionRoute { get; set; }

    /// <summary>
    /// 接受触发的机器人账号
    /// </summary>
    public string BotAccount { get; set; }

    /// <summary>
    /// 触发事件的平台
    /// </summary>
    public string TargetPlatform { get; set; }

    /// <summary>
    /// 事件类型
    /// </summary>
    public EventType MessageEventType { get; set; }

    /// <summary>
    /// 触发这个消息的个体Id，如果触发消息的是非个体，那么会使用 AlterTriggerId 的枚举类型
    /// </summary>
    public string TriggerId { get; set; }

    /// <summary>
    /// 来源组信息：
    /// 如果是个体信息，那么这个值等于 TriggerId;
    /// 如果是群聊信息，那么这个值等于群组的账号;
    /// 如果是频道信息，那么这个值等于频道的主账号;
    /// </summary>
    public string TriggerPlatformId { get; set; }

    /// <summary>
    /// 预留的辅助 Id，具体语境根据 TriggerPlatformId 有具体的定义，例如其为频道的主账号时，那么 TiedId 表示的时频道的子频道账号
    /// </summary>
    public string TiedId { get; set; }

    /// <summary>
    /// 长对话支持模块，可以不存在
    /// </summary>
    public LongMessageContext? LongMessageContext { get; set; }

    /// <summary>
    /// 消息实体，使用本对象应该遵循语义为主的处理方式
    /// </summary>
    public MessageEntity? Message { get; set; }

    /// <summary>
    /// 原始命令参数列表，本列表会存储任何原始的参数信息（不含有母命名头）。
    /// 请仅在无法通过参数注入的情况下使用本命令。
    /// 参数的 Key 为特性注入时提供的参数 Key
    /// </summary>
    public Dictionary<string, string?>? CommandParas { get; set; } = new Dictionary<string, string?>();

    /// <summary>
    /// 表示消息附带的属性，此属性不被框架使用，为消息预留
    /// </summary>
    public string TiedAttribute { get; set; }

    /// <summary>
    /// 表示消息携带的针对于平台的属性
    /// </summary>
    public Dictionary<string, string> UnderProperty { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// 表示消息产生的时间戳
    /// </summary>
    public string MessageTime { get; set; }

    public MessageContext(string action, string botAccount, string targetPlatform, EventType messageEventType,
        string triggerId, string triggerPlatformId, string tiedId, MessageEntity message)
    {
        ActionRoute = action;
        BotAccount = botAccount;
        TargetPlatform = targetPlatform;
        MessageEventType = messageEventType;
        TriggerId = triggerId;
        TriggerPlatformId = triggerPlatformId;
        TiedId = tiedId;
        Message = message;
    }

    public MessageContext(string action, string botAccount, string targetPlatform, EventType messageEventType,
        string triggerId, string triggerPlatformId, string tiedId, LongMessageContext? longMessageContext,
        MessageEntity message)
    {
        ActionRoute = action;
        BotAccount = botAccount;
        TargetPlatform = targetPlatform;
        MessageEventType = messageEventType;
        TriggerId = triggerId;
        TriggerPlatformId = triggerPlatformId;
        TiedId = tiedId;
        LongMessageContext = longMessageContext;
        Message = message;
    }

    public MessageContext(string action, string botAccount, string targetPlatform, EventType messageEventType,
        string triggerId, string triggerPlatformId, string tiedId, MessageEntity message,
        Dictionary<string, string?>? commandParas)
    {
        ActionRoute = action;
        BotAccount = botAccount;
        TargetPlatform = targetPlatform;
        MessageEventType = messageEventType;
        TriggerId = triggerId;
        TriggerPlatformId = triggerPlatformId;
        TiedId = tiedId;
        Message = message;
        CommandParas = commandParas;
    }

    public MessageContext(string action, string botAccount, string targetPlatform, EventType messageEventType,
        string triggerId, string triggerPlatformId, string tiedId, LongMessageContext? longMessageContext,
        MessageEntity message, Dictionary<string, string?>? commandParas)
    {
        ActionRoute = action;
        BotAccount = botAccount;
        TargetPlatform = targetPlatform;
        MessageEventType = messageEventType;
        TriggerId = triggerId;
        TriggerPlatformId = triggerPlatformId;
        TiedId = tiedId;
        LongMessageContext = longMessageContext;
        Message = message;
        CommandParas = commandParas;
    }

    public MessageContext()
    {
    }
}