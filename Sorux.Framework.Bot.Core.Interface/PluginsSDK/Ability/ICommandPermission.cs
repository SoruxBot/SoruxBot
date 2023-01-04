namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;
/// <summary>
/// 实现本接口才会向框架请求权限接管
/// </summary>
public interface ICommandPermission
{
    /// <summary>
    /// 用户执行时因为权限被拒绝而发送的消息
    /// 消息格式：[艾特行为][空格][反馈消息][换行]["缺少的权限节点是："][权限节点]
    /// </summary>
    /// <returns></returns>
    public string GetPermissionDeniedMessage();
    /// <summary>
    /// 权限不够时是否会艾特用户
    /// </summary>
    /// <returns></returns>
    public bool IsPermissionDeniedAutoAt();
    /// <summary>
    /// 权限不够时是否通知用户对应的权限节点是什么
    /// </summary>
    /// <returns></returns>
    public bool IsPermissionDeniedLeakOut();
    /// <summary>
    /// 权限不够时是否自动反馈权限不够的提示
    /// </summary>
    /// <returns></returns>
    public bool IsPermissionDeniedAutoReply();
}