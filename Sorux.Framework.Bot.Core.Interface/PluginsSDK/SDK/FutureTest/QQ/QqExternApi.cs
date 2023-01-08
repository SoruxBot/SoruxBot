using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;

/// <summary>
/// 对 QQ 的 API 进行解析
/// </summary>
public static class QqExternApi
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="userId">对方 QQ 号</param>
    /// <param name="groupId">主动发起临时会话时的来源群号(可选, 机器人本身必须是管理员/群主)</param>
    /// <param name="message">要发送的内容</param>
    /// <param name="autoEscape">消息内容是否作为纯文本发送 ( 即不解析 CQ 码 ) , 只在 message 字段是字符串时有效</param>
    /// <returns>message_id:消息 ID</returns>
    public static string QqSendPrivateMessageCompute(this IBasicAPI basicApi,MessageContext context,string userId,string? groupId,string message,bool? autoEscape)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = userId;
        json["group_id"] = groupId;
        json["message"] = message;
        json["auto_escape"] = autoEscape == null ? null : autoEscape.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_private_msg"
        };
        return basicApi.ActionCompute(response);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="groupId">群号</param>
    /// <param name="message">要发送的内容</param>
    /// <param name="autoEscape">消息内容是否作为纯文本发送 ( 即不解析 CQ 码 ) , 只在 message 字段是字符串时有效</param>
    /// <returns>message_id:消息 ID</returns>
    public static string QqSendGroupMessageCompute(this IBasicAPI basicApi,MessageContext context,string? groupId,string message,bool? autoEscape)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = groupId;
        json["message"] = message;
        json["auto_escape"] = autoEscape == null ? null : autoEscape.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_group_msg"
        };
        return basicApi.ActionCompute(response);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="messageId">消息 ID</param>
    /// <returns>空</returns>
    public static string QqRecallMessageCompute(this IBasicAPI basicApi,MessageContext context,string messageId)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = messageId;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;delete_msg"
        };
        return basicApi.ActionCompute(response);
    }
}