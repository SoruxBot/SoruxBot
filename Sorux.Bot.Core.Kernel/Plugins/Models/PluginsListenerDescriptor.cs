using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Interface.PluginsSDK.PluginsModels;

namespace Sorux.Bot.Core.Kernel.Plugins.Models;

public class PluginsListenerDescriptor
{
    /// <summary>
    /// 监听事件的类型
    /// </summary>
    public EventType eventType { get; set; }

    /// <summary>
    /// 监听事件的平台
    /// </summary>
    public string? targetPlatform { get; set; }

    /// <summary>
    /// 监听事件的方法
    /// </summary>
    public string? targetAction { get; set; }

    /// <summary>
    /// 监听事件的条件
    /// </summary>
    public Func<MessageContext, bool> action { get; set; }

    /// <summary>
    /// 是否禁止消息继续传递，相当于Action返回了PluginsFucFlag.Intercept，且同时禁止了其他的监听器监听消息
    /// </summary>
    public bool isIntercept { get; set; }

    /// <summary>
    /// 设置消息的状态，且继续使其他监听器监听消息
    /// </summary>
    public PluginFucFlag flag { get; set; }

    /// <summary>
    /// 下一个CTX
    /// </summary>
    public MessageContext? nextContext { get; set; }
}