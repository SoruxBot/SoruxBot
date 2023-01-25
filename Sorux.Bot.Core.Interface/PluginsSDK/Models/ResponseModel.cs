namespace Sorux.Bot.Core.Interface.PluginsSDK.Models;

public class ResponseModel
{
    /// <summary>
    /// 表示消息ID
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// 表示消息的内容
    /// </summary>
    public string? MessageContent { get; set; }

    /// <summary>
    /// 表示接收者Id，如果是普通群聊那么此属性为空
    /// </summary>
    public string? Receiver { get; set; }

    /// <summary>
    /// 表示接收者附属的平台Id
    /// </summary>
    public string? GroupId { get; set; }

    /// <summary>
    /// 表示隐藏属性
    /// </summary>
    public Dictionary<string, string> UnderProperty { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// 表示API请求地址
    /// </summary>
    public string? ResopnseRoute { get; set; }

    /// <summary>
    /// 表示原始请求
    /// </summary>
    public string RawContent { get; set; }
}