using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

/// <summary>
/// 长对话支持模块，需要框架本身启用长对话监听
/// </summary>
public interface ILongMessageCommunicate
{
    /// <summary>
    /// 以异步方式快速创建一个监听私聊消息的监听器
    /// </summary>
    /// <param name="type"></param>
    /// <param name="context"></param>
    /// <param name="timeOut">最长等待时间</param>
    /// <returns></returns>
    public Task<MessageContext?> ReadNextPrivateMessageAsync(MessageContext context, int? timeOut);

    /// <summary>
    /// 以异步方式快速创建一个根据指定类型监听群聊消息的监听器
    /// </summary>
    /// <param name="type"></param>
    /// <param name="context"></param>
    /// <param name="timeOut">最长等待时间</param>
    /// <returns></returns>
    public Task<MessageContext?> ReadNextGroupMessageAsync(LongCommunicateType type, MessageContext context,
        int? timeOut);

    /// <summary>
    /// 以异步方式通过单条件自定义组装泛型监听器
    /// </summary>
    /// <param name="eventType">消息的类型</param>
    /// <param name="targetPlatform">消息的指定平台</param>
    /// <param name="targetAction">消息的指定动作</param>
    /// <param name="action">委托，根据传入元素判断是否被监听</param>
    /// <param name="isIntercept">判断是否被拦截</param>
    /// <param name="flag">若不被拦截，消息在管道中的状态</param>
    /// <param name="timeOut">最长等待时间</param>
    /// <returns></returns>
    public Task<MessageContext?> CreateGenericListenerAsync(EventType eventType, string? targetPlatform,
        string? targetAction,
        Func<MessageContext, bool> action, bool isIntercept, PluginFucFlag flag, int? timeOut);
}