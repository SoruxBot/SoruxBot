namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

public class ResponseContext
{
    /// <summary>
    /// 表示 Response 所携带的 Message 对象
    /// </summary>
    public MessageContext Message { get; init; }
    /// <summary>
    /// 表示 Resopnse 的指向 路由地址
    /// </summary>
    // 本属性格式为 Platform;Url 例如 qq;sendPrivateMessage
    // 如果是通用平台即为 common;sendPrivateMessage 然后传递给协议对象的 Wrapper 进行下一步处理
    // 如果是平台特定，例如 qq;sendPrivateMessage 那么就直接传递给协议层对象的实例
    public string ResponseRoute { get; init; }
    /// <summary>
    /// 表示 Response 携带的数据，该数据会被 Post 到指定的路由地址
    /// </summary>
    public ResponseModel ResponseData { get; init; }
    /// <summary>
    /// 框架预留的属性，用于记录额外信息
    /// </summary>
    public string Tag { get; set; }
    /// <summary>
    /// 为任意实体
    /// 为Json格式的数据，当框架无法解析的时候，以此属性直接作为Json参数传递到指定的平台协议实体
    /// </summary>
    public object TargetPlatfromJson { get; set; }
}