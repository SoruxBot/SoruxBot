using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;

/// <summary>
/// 对 QQ 的 API 进行解析
/// </summary>
public static class QqExternApi
{
    /// <summary>
    /// 发送私聊消息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="userId">对方 QQ 号</param>
    /// <param name="groupId">主动发起临时会话时的来源群号(可选, 机器人本身必须是管理员/群主)</param>
    /// <param name="message">要发送的内容</param>
    /// <param name="autoEscape">消息内容是否作为纯文本发送 ( 即不解析 CQ 码 ) , 只在 message 字段是字符串时有效</param>
    /// <returns>message_id:消息 ID</returns>
    public static Task<string> QqSendPrivateMessageAsync(this IBasicAPI basicApi, MessageContext context, string userId,
        string? groupId, string message, bool? autoEscape)
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
        return basicApi.ActionAsync(response);
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
    public static Task<string> QqSendGroupMessageAsync(this IBasicAPI basicApi, MessageContext context, string? groupId,
        string message, bool? autoEscape)
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
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="messageId">消息 ID</param>
    /// <returns>空</returns>
    public static Task<string> QqRecallMessageAsync(this IBasicAPI basicApi, MessageContext context, string messageId)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = messageId;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;delete_msg"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 发送合并转发 ( 群 )
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="messages">自定义转发消息</param>
    /// <returns></returns>
    public static Task<string> QqSendGroupForwardAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? messages)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["messages"] = messages;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_group_forward_msg"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取消息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="message_id">消息id</param>
    /// <returns>group	bool	是否是群消息；group_id	int64	是群消息时的群号(否则不存在此字段)；message_id	int32	消息id;real_id	int32	消息真实id;message_type	string	群消息时为group, 私聊消息为private;sender	object	发送者;time	int32	发送时间;message	message	消息内容;raw_message	message	原始消息内容</returns>
    public static Task<string> QqGetMessageAsync(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_msg"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="message_id">消息id</param>
    /// <returns></returns>
    public static Task<string> QqGetMessageForwardAsync(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_forward_msg"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取图片信息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="message_id">消息id</param>
    /// <returns></returns>
    public static Task<string> QqMarkMessageAsReadAsync(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;mark_msg_as_read"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="user_id">要踢的 QQ 号</param>
    /// <param name="reject_add_request">拒绝此人的加群请求</param>
    /// <returns></returns>
    public static Task<string> QqGetImageAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? reject_add_request)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["reject_add_request"] = reject_add_request == null ? "" : reject_add_request.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_kick"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="user_id">要禁言的 QQ 号</param>
    /// <param name="duration">禁言时长, 单位秒, 0 表示取消禁言</param>
    /// <returns></returns>
    public static Task<string> QqSetGroupBanAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_ban"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="anonymous">可选, 要禁言的匿名用户对象（群消息上报的 anonymous 字段）</param>
    /// <param name="flag">可选, 要禁言的匿名用户的 flag（需从群消息上报的数据中获得）</param>
    /// <param name="duration">禁言时长, 单位秒, 无法取消匿名用户禁言</param>
    /// <returns></returns>
    public static Task<string> QqSetGroupAnonymousBanAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? anonymous, string? flag, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["anonymous"] = anonymous;
        json["flag"] = flag;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_anonymous_ban"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="enable">是否禁言</param>
    /// <returns></returns>
    public static Task<string> QqSetAllGroupBanAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? enable)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["enable"] = enable == null ? "" : enable.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_whole_ban"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="user_id">要设置管理员的 QQ 号</param>
    /// <param name="enable">true 为设置, false 为取消</param>
    /// <returns></returns>
    public static Task<string> QqSetGroupAdminAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? enable)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["enable"] = enable == null ? "" : enable.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_admin"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">	群号</param>
    /// <param name="user_id">要设置的 QQ 号</param>
    /// <param name="card">群名片内容, 不填或空字符串表示删除群名片</param>
    /// <returns></returns>
    public static Task<string> QqSetGroupCardAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, string? card)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["card"] = card;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_card"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="group_name">新群名</param>
    /// <returns></returns>
    public static Task<string> QqSetGroupNameAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? group_name)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["group_name"] = group_name;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_name"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 退出群组
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="is_dismiss">是否解散, 如果登录号是群主, 则仅在此项为 true 时能够解散</param>
    /// <returns></returns>
    public static Task<string> QqLeaveGroupAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? is_dismiss)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["is_dismiss"] = is_dismiss == null ? "" : is_dismiss.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_leave"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 设置群组专属头衔
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="user_id">要设置的 QQ 号</param>
    /// <param name="special_title">专属头衔, 不填或空字符串表示删除专属头衔</param>
    /// <param name="duration">专属头衔有效期, 单位秒, -1 表示永久, 不过此项似乎没有效果, 可能是只有某些特殊的时间长度有效, 有待测试</param>
    /// <returns></returns>
    public static Task<string> QqSetGroupSpecialTitleAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, string? special_title, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["special_title"] = special_title;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_special_title"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 群打卡
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <returns></returns>
    public static Task<string> QqSendGroupSignAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_group_sign"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 处理加好友请求
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="flag">加好友请求的 flag（需从上报的数据中获得）</param>
    /// <param name="approve">是否同意请求</param>
    /// <param name="mark">添加后的好友备注（仅在同意时有效）</param>
    /// <returns></returns>
    public static Task<string> QqSetFriendAddRequestAsync(this IBasicAPI basicApi, MessageContext context,
        string? flag, bool? approve, string? mark)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["flag"] = flag;
        json["approve"] = approve == null ? "" : approve.ToString();
        json["mark"] = mark;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_friend_add_request"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 处理加群请求／邀请
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="flag">加群请求的 flag（需从上报的数据中获得）</param>
    /// <param name="sub_type">add 或 invite, 请求类型（需要和上报消息中的 sub_type 字段相符）</param>
    /// <param name="approve">是否同意请求／邀请</param>
    /// <param name="reason">拒绝理由（仅在拒绝时有效）</param>
    /// <returns></returns>
    public static Task<string> QqSetGroupAddRequestAsync(this IBasicAPI basicApi, MessageContext context,
        string? flag, string? sub_type, bool? approve, string? reason)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["flag"] = flag;
        json["approve"] = approve == null ? "" : approve.ToString();
        json["sub_type"] = sub_type;
        json["reason"] = reason;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_add_request"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取登录号信息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Task<string> QqGetLoginInfoAsync(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_login_info"
        };
        return basicApi.ActionAsync(response);
    }


    /// <summary>
    /// 获取企点账号信息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static Task<string> QqGetQiDianInfoAsync(this IBasicAPI basicApi, MessageContext context,
        string? file)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;qidian_get_account_info"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 设置登录号资料
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="nickname">名称</param>
    /// <param name="company">公司</param>
    /// <param name="email">公司</param>
    /// <param name="college">学校</param>
    /// <param name="personal_note">个人说明</param>
    /// <returns></returns>
    public static Task<string> QqSetQQProfileAsync(this IBasicAPI basicApi, MessageContext context,
        string? nickname, string? company, string? email, string? college, string? personal_note)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["nickname"] = nickname;
        json["company"] = company;
        json["email"] = email;
        json["college"] = college;
        json["personal_note"] = personal_note;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_qq_profile"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取陌生人信息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="user_id">QQ 号</param>
    /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时, 但响应更快）</param>
    /// <returns></returns>
    public static Task<string> QqGetStrangeInfoAsync(this IBasicAPI basicApi, MessageContext context,
        string? user_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_stranger_info"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取好友列表
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Task<string> QqGetFriendListAsync(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_friend_list"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取单向好友列表
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Task<string>? QqGetUnidirectionalFriendListAsync(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_unidirectional_friend_list"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 删除好友
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="user_id">好友 QQ 号</param>
    /// <returns></returns>
    public static Task<string> QqDeleteFrinedAsync(this IBasicAPI basicApi, MessageContext context,
        string? user_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;delete_friend"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取群信息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时, 但响应更快）</param>
    /// <returns></returns>
    public static Task<string> QqGetGroupInfoAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_info"
        };
        return basicApi.ActionAsync(response);
    }

    public static Task<string> QqGetGroupMemberAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_member_info"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取群成员列表
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="no_cache"></param>
    /// <returns></returns>
    public static Task<string> QqGetGroupMemberListAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_member_list"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 图片 OCR
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public static Task<string> QqOcrImageAsync(this IBasicAPI basicApi, MessageContext context,
        string? image)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["image"] = image;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;ocr_image"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取群系统消息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="user_id"></param>
    /// <returns></returns>
    public static Task<string> QqGetGroupSystemMessageAsync(this IBasicAPI basicApi, MessageContext context)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_group_system_msg"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 上传私聊文件
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="user_id"></param>
    /// <param name="file">本地文件路径</param>
    /// <param name="name">文件名称</param>
    /// <returns></returns>
    public static Task<string> QqUploadPrivateMessageAsync(this IBasicAPI basicApi, MessageContext context,
        string? user_id, string? file, string? name)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        json["file"] = file;
        json["name"] = name;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;upload_private_file"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 上传群文件
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id"><群号/param>
    /// <param name="file">本地文件路径</param>
    /// <param name="name">储存名称</param>
    /// <param name="folder">父目录ID</param>
    /// <returns></returns>
    public static Task<string> QqUploadGroupFileAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? file, string? name, string? folder)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["file"] = file;
        json["name"] = name;
        json["folder"] = folder;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;upload_group_file"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 发送群公告
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <param name="content">群号</param>
    /// <param name="image">图片路径（可选）</param>
    /// <returns></returns>
    public static Task<string> QqSendGroupNoticeAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? content, string? image)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["content"] = content;
        json["image"] = image;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;_send_group_notice"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取群公告
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <returns></returns>
    public static Task<string> QqGetGroupNoticeAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;group_id"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 设置精华消息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="message_id">消息ID</param>
    /// <returns></returns>
    public static Task<string> QqSetEssenceMessageAsync(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_essence_msg"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 移出精华消息
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="message_id">消息ID</param>
    /// <returns></returns>
    public static Task<string> QqRemoveEssenceMessageAsync(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;message_id"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 获取精华消息列表
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="group_id">群号</param>
    /// <returns></returns>
    public static Task<string> QqGetImageAsync(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_essence_msg_list"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>
    /// 检查链接安全性
    /// </summary>
    /// <param name="basicApi"></param>
    /// <param name="context"></param>
    /// <param name="url">需要检查的链接</param>
    /// <returns>安全等级, 1: 安全 2: 未知 3: 危险</returns>
    public static Task<string> QqCheckLinkSafetyAsync(this IBasicAPI basicApi, MessageContext context,
        string? url)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["url"] = url;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;url"
        };
        return basicApi.ActionAsync(response);
    }

    /// <summary>  
    /// 发送私聊消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="userId">对方 QQ 号</param>  
    /// <param name="groupId">主动发起临时会话时的来源群号(可选, 机器人本身必须是管理员/群主)</param>  
    /// <param name="message">要发送的内容</param>  
    /// <param name="autoEscape">消息内容是否作为纯文本发送 ( 即不解析 CQ 码 ) , 只在 message 字段是字符串时有效</param>  
    /// <returns>message_id:消息 ID</returns>
    public static string QqSendPrivateMessageCompute(this IBasicAPI basicApi, MessageContext context, string userId,
        string? groupId, string message, bool? autoEscape)
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
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="groupId">群号</param>  
    /// <param name="message">要发送的内容</param>  
    /// <param name="autoEscape">消息内容是否作为纯文本发送 ( 即不解析 CQ 码 ) , 只在 message 字段是字符串时有效</param>  
    /// <returns>message_id:消息 ID</returns>
    public static string QqSendGroupMessageCompute(this IBasicAPI basicApi, MessageContext context, string? groupId,
        string message, bool? autoEscape)
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
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="messageId">消息 ID</param>/// <returns>空</returns>  
    public static string QqRecallMessageCompute(this IBasicAPI basicApi, MessageContext context, string messageId)
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

    /// <summary>  
    /// 发送合并转发 ( 群 )/// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="messages">自定义转发消息</param>  
    /// <returns></returns>  
    public static string QqSendGroupForwardCompute(this IBasicAPI basicApi, MessageContext context, string? group_id,
        string? messages)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["messages"] = messages;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_group_forward_msg"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息id</param>  
    /// <returns>group bool   是否是群消息；group_id    int64  是群消息时的群号(否则不存在此字段)；message_id  int32  消息id;real_id   int32  消息真实id;message_type    string 群消息时为group, 私聊消息为private;sender    object 发送者;time   int32  发送时间;message   message    消息内容;raw_message   message    原始消息内容</returns>  
    public static string QqGetMessageCompute(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_msg"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息id</param>  
    /// <returns></returns>  
    public static string QqGetMessageForwardCompute(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_forward_msg"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取图片信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息id</param>  
    /// <returns></returns>  
    public static string QqMarkMessageAsReadCompute(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;mark_msg_as_read"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要踢的 QQ 号</param>  
    /// <param name="reject_add_request">拒绝此人的加群请求</param>  
    /// <returns></returns>  
    public static string QqGetImageCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? reject_add_request)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["reject_add_request"] = reject_add_request == null ? "" : reject_add_request.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_kick"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要禁言的 QQ 号</param>  
    /// <param name="duration">禁言时长, 单位秒, 0 表示取消禁言</param>  
    /// <returns></returns>  
    public static string QqSetGroupBanCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_ban"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="anonymous">可选, 要禁言的匿名用户对象（群消息上报的 anonymous 字段）</param>  
    /// <param name="flag">可选, 要禁言的匿名用户的 flag（需从群消息上报的数据中获得）</param>  
    /// <param name="duration">禁言时长, 单位秒, 无法取消匿名用户禁言</param>  
    /// <returns></returns>  
    public static string QqSetGroupAnonymousBanCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? anonymous, string? flag, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["anonymous"] = anonymous;
        json["flag"] = flag;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_anonymous_ban"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="enable">是否禁言</param>  
    /// <returns></returns>  
    public static string QqSetAllGroupBanCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? enable)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["enable"] = enable == null ? "" : enable.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_whole_ban"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要设置管理员的 QQ 号</param>  
    /// <param name="enable">true 为设置, false 为取消</param>  
    /// <returns></returns>  
    public static string QqSetGroupAdminCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? enable)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["enable"] = enable == null ? "" : enable.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_admin"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">    群号</param>  
    /// <param name="user_id">要设置的 QQ 号</param>  
    /// <param name="card">群名片内容, 不填或空字符串表示删除群名片</param>  
    /// <returns></returns>  
    public static string QqSetGroupCardCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, string? card)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["card"] = card;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_card"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="group_name">新群名</param>  
    /// <returns></returns>  
    public static string QqSetGroupNameCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? group_name)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["group_name"] = group_name;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_name"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 退出群组  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="is_dismiss">是否解散, 如果登录号是群主, 则仅在此项为 true 时能够解散</param>  
    /// <returns></returns>  
    public static string QqLeaveGroupCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? is_dismiss)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["is_dismiss"] = is_dismiss == null ? "" : is_dismiss.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_leave"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 设置群组专属头衔  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要设置的 QQ 号</param>  
    /// <param name="special_title">专属头衔, 不填或空字符串表示删除专属头衔</param>  
    /// <param name="duration">专属头衔有效期, 单位秒, -1 表示永久, 不过此项似乎没有效果, 可能是只有某些特殊的时间长度有效, 有待测试</param>  
    /// <returns></returns>  
    public static string QqSetGroupSpecialTitleCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, string? special_title, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["special_title"] = special_title;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_special_title"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 群打卡  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <returns></returns>  
    public static string QqSendGroupSignCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_group_sign"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 处理加好友请求  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="flag">加好友请求的 flag（需从上报的数据中获得）</param>  
    /// <param name="approve">是否同意请求</param>  
    /// <param name="mark">添加后的好友备注（仅在同意时有效）</param>  
    /// <returns></returns>  
    public static string QqSetFriendAddRequestCompute(this IBasicAPI basicApi, MessageContext context,
        string? flag, bool? approve, string? mark)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["flag"] = flag;
        json["approve"] = approve == null ? "" : approve.ToString();
        json["mark"] = mark;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_friend_add_request"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 处理加群请求／邀请  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="flag">加群请求的 flag（需从上报的数据中获得）</param>  
    /// <param name="sub_type">add 或 invite, 请求类型（需要和上报消息中的 sub_type 字段相符）</param>  
    /// <param name="approve">是否同意请求／邀请</param>  
    /// <param name="reason">拒绝理由（仅在拒绝时有效）</param>  
    /// <returns></returns>  
    public static string QqSetGroupAddRequestCompute(this IBasicAPI basicApi, MessageContext context,
        string? flag, string? sub_type, bool? approve, string? reason)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["flag"] = flag;
        json["approve"] = approve == null ? "" : approve.ToString();
        json["sub_type"] = sub_type;
        json["reason"] = reason;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_add_request"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取登录号信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <returns></returns>  
    public static string QqGetLoginInfoCompute(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_login_info"
        };
        return basicApi.ActionCompute(response);
    }


    /// <summary>  
    /// 获取企点账号信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="file"></param>  
    /// <returns></returns>  
    public static string QqGetQiDianInfoCompute(this IBasicAPI basicApi, MessageContext context,
        string? file)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;qidian_get_account_info"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 设置登录号资料  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="nickname">名称</param>  
    /// <param name="company">公司</param>  
    /// <param name="email">公司</param>  
    /// <param name="college">学校</param>  
    /// <param name="personal_note">个人说明</param>  
    /// <returns></returns>  
    public static string QqSetQQProfileCompute(this IBasicAPI basicApi, MessageContext context,
        string? nickname, string? company, string? email, string? college, string? personal_note)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["nickname"] = nickname;
        json["company"] = company;
        json["email"] = email;
        json["college"] = college;
        json["personal_note"] = personal_note;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_qq_profile"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取陌生人信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id">QQ 号</param>  
    /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时, 但响应更快）</param>  
    /// <returns></returns>  
    public static string QqGetStrangeInfoCompute(this IBasicAPI basicApi, MessageContext context,
        string? user_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_stranger_info"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取好友列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <returns></returns>  
    public static List<Friend>? QqGetFriendListCompute(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_friend_list"
        };
        return JsonConvert.DeserializeObject<List<Friend>>(basicApi.ActionCompute(response));
    }

    /// <summary>  
    /// 获取单向好友列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <returns></returns>  
    public static List<Friend>? QqGetUnidirectionalFriendListCompute(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_unidirectional_friend_list"
        };
        return JsonConvert.DeserializeObject<List<Friend>>(basicApi.ActionCompute(response));
    }

    /// <summary>  
    /// 删除好友  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id">好友 QQ 号</param>  
    /// <returns></returns>  
    public static string QqDeleteFrinedCompute(this IBasicAPI basicApi, MessageContext context,
        string? user_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;delete_friend"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取群信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时, 但响应更快）</param>  
    /// <returns></returns>  
    public static GroupInfo? QqGetGroupInfoCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_info"
        };
        return JsonConvert.DeserializeObject<GroupInfo>(basicApi.ActionCompute(response));
    }

    public static GroupMember? QqGetGroupMemberCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_member_info"
        };
        return JsonConvert.DeserializeObject<GroupMember>(basicApi.ActionCompute(response));
    }

    /// <summary>  
    /// 获取群成员列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id"></param>  
    /// <param name="user_id"></param>  
    /// <param name="no_cache"></param>  
    /// <returns></returns>  
    public static List<GroupMember>? QqGetGroupMemberListCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_member_list"
        };
        return JsonConvert.DeserializeObject<List<GroupMember>>(basicApi.ActionCompute(response));
    }

    /// <summary>  
    /// 图片 OCR/// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="image"></param>  
    /// <returns></returns>  
    public static ImageOcrModel? QqOcrImageCompute(this IBasicAPI basicApi, MessageContext context,
        string? image)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["image"] = image;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;ocr_image"
        };
        return JsonConvert.DeserializeObject<ImageOcrModel>(basicApi.ActionCompute(response));
    }

    /// <summary>  
    /// 获取群系统消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id"></param>  
    /// <returns></returns>  
    public static string QqGetGroupSystemMessageCompute(this IBasicAPI basicApi, MessageContext context)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_group_system_msg"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 上传私聊文件  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id"></param>  
    /// <param name="file">本地文件路径</param>  
    /// <param name="name">文件名称</param>  
    /// <returns></returns>  
    public static string QqUploadPrivateMessageCompute(this IBasicAPI basicApi, MessageContext context,
        string? user_id, string? file, string? name)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        json["file"] = file;
        json["name"] = name;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;upload_private_file"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 上传群文件  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id"><群号/param>  
    /// <param name="file">本地文件路径</param>  
    /// <param name="name">储存名称</param>  
    /// <param name="folder">父目录ID</param>  
    /// <returns></returns>  
    public static string QqUploadGroupFileCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? file, string? name, string? folder)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["file"] = file;
        json["name"] = name;
        json["folder"] = folder;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;upload_group_file"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 发送群公告  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="content">群号</param>  
    /// <param name="image">图片路径（可选）</param>  
    /// <returns></returns>  
    public static string QqSendGroupNoticeCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? content, string? image)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["content"] = content;
        json["image"] = image;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;_send_group_notice"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取群公告  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <returns></returns>  
    public static string QqGetGroupNoticeCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;group_id"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 设置精华消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息ID</param>  
    /// <returns></returns>  
    public static string QqSetEssenceMessageCompute(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_essence_msg"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 移出精华消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息ID</param>  
    /// <returns></returns>  
    public static string QqRemoveEssenceMessageCompute(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;message_id"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 获取精华消息列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <returns></returns>  
    public static string QqGetImageCompute(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_essence_msg_list"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 检查链接安全性  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="url">需要检查的链接</param>  
    /// <returns>安全等级, 1: 安全 2: 未知 3: 危险</returns>  
    public static string QqCheckLinkSafetyCompute(this IBasicAPI basicApi, MessageContext context,
        string? url)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["url"] = url;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;url"
        };
        return basicApi.ActionCompute(response);
    }

    /// <summary>  
    /// 发送私聊消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="userId">对方 QQ 号</param>  
    /// <param name="groupId">主动发起临时会话时的来源群号(可选, 机器人本身必须是管理员/群主)</param>  
    /// <param name="message">要发送的内容</param>  
    /// <param name="autoEscape">消息内容是否作为纯文本发送 ( 即不解析 CQ 码 ) , 只在 message 字段是字符串时有效</param>  
    /// <returns>message_id:消息 ID</returns>
    public static void QqSendPrivateMessage(this IBasicAPI basicApi, MessageContext context, string userId,
        string? groupId, string message, bool? autoEscape)
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
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="groupId">群号</param>  
    /// <param name="message">要发送的内容</param>  
    /// <param name="autoEscape">消息内容是否作为纯文本发送 ( 即不解析 CQ 码 ) , 只在 message 字段是字符串时有效</param>  
    /// <returns>message_id:消息 ID</returns>
    public static void QqSendGroupMessage(this IBasicAPI basicApi, MessageContext context, string? groupId,
        string message, bool? autoEscape)
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
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="messageId">消息 ID</param>/// <returns>空</returns>  
    public static void QqRecallMessage(this IBasicAPI basicApi, MessageContext context, string messageId)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = messageId;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;delete_msg"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 发送合并转发 ( 群 )/// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="messages">自定义转发消息</param>  
    /// <returns></returns>  
    public static void QqSendGroupForward(this IBasicAPI basicApi, MessageContext context, string? group_id,
        string? messages)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["messages"] = messages;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_group_forward_msg"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息id</param>  
    /// <returns>group bool   是否是群消息；group_id    int64  是群消息时的群号(否则不存在此字段)；message_id  int32  消息id;real_id   int32  消息真实id;message_type    string 群消息时为group, 私聊消息为private;sender    object 发送者;time   int32  发送时间;message   message    消息内容;raw_message   message    原始消息内容</returns>  
    public static void QqGetMessage(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_msg"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息id</param>  
    /// <returns></returns>  
    public static void QqGetMessageForward(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_forward_msg"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取图片信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息id</param>  
    /// <returns></returns>  
    public static void QqMarkMessageAsRead(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;mark_msg_as_read"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要踢的 QQ 号</param>  
    /// <param name="reject_add_request">拒绝此人的加群请求</param>  
    /// <returns></returns>  
    public static void QqGetImage(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? reject_add_request)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["reject_add_request"] = reject_add_request == null ? "" : reject_add_request.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_kick"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要禁言的 QQ 号</param>  
    /// <param name="duration">禁言时长, 单位秒, 0 表示取消禁言</param>  
    /// <returns></returns>  
    public static void QqSetGroupBan(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_ban"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="anonymous">可选, 要禁言的匿名用户对象（群消息上报的 anonymous 字段）</param>  
    /// <param name="flag">可选, 要禁言的匿名用户的 flag（需从群消息上报的数据中获得）</param>  
    /// <param name="duration">禁言时长, 单位秒, 无法取消匿名用户禁言</param>  
    /// <returns></returns>  
    public static void QqSetGroupAnonymousBan(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? anonymous, string? flag, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["anonymous"] = anonymous;
        json["flag"] = flag;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_anonymous_ban"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="enable">是否禁言</param>  
    /// <returns></returns>  
    public static void QqSetAllGroupBan(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? enable)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["enable"] = enable == null ? "" : enable.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_whole_ban"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要设置管理员的 QQ 号</param>  
    /// <param name="enable">true 为设置, false 为取消</param>  
    /// <returns></returns>  
    public static void QqSetGroupAdmin(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? enable)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["enable"] = enable == null ? "" : enable.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_admin"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">    群号</param>  
    /// <param name="user_id">要设置的 QQ 号</param>  
    /// <param name="card">群名片内容, 不填或空字符串表示删除群名片</param>  
    /// <returns></returns>  
    public static void QqSetGroupCard(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, string? card)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["card"] = card;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_card"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="group_name">新群名</param>  
    /// <returns></returns>  
    public static void QqSetGroupName(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? group_name)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["group_name"] = group_name;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_name"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 退出群组  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="is_dismiss">是否解散, 如果登录号是群主, 则仅在此项为 true 时能够解散</param>  
    /// <returns></returns>  
    public static void QqLeaveGroup(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? is_dismiss)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["is_dismiss"] = is_dismiss == null ? "" : is_dismiss.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_leave"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 设置群组专属头衔  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="user_id">要设置的 QQ 号</param>  
    /// <param name="special_title">专属头衔, 不填或空字符串表示删除专属头衔</param>  
    /// <param name="duration">专属头衔有效期, 单位秒, -1 表示永久, 不过此项似乎没有效果, 可能是只有某些特殊的时间长度有效, 有待测试</param>  
    /// <returns></returns>  
    public static void QqSetGroupSpecialTitle(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, string? special_title, int? duration)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["special_title"] = special_title;
        json["duration"] = duration == null ? "" : duration.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_special_title"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 群打卡  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <returns></returns>  
    public static void QqSendGroupSign(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;send_group_sign"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 处理加好友请求  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="flag">加好友请求的 flag（需从上报的数据中获得）</param>  
    /// <param name="approve">是否同意请求</param>  
    /// <param name="mark">添加后的好友备注（仅在同意时有效）</param>  
    /// <returns></returns>  
    public static void QqSetFriendAddRequest(this IBasicAPI basicApi, MessageContext context,
        string? flag, bool? approve, string? mark)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["flag"] = flag;
        json["approve"] = approve == null ? "" : approve.ToString();
        json["mark"] = mark;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_friend_add_request"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 处理加群请求／邀请  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="flag">加群请求的 flag（需从上报的数据中获得）</param>  
    /// <param name="sub_type">add 或 invite, 请求类型（需要和上报消息中的 sub_type 字段相符）</param>  
    /// <param name="approve">是否同意请求／邀请</param>  
    /// <param name="reason">拒绝理由（仅在拒绝时有效）</param>  
    /// <returns></returns>  
    public static void QqSetGroupAddRequest(this IBasicAPI basicApi, MessageContext context,
        string? flag, string? sub_type, bool? approve, string? reason)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["flag"] = flag;
        json["approve"] = approve == null ? "" : approve.ToString();
        json["sub_type"] = sub_type;
        json["reason"] = reason;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_group_add_request"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取登录号信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <returns></returns>  
    public static void QqGetLoginInfo(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_login_info"
        };
        basicApi.Action(response);
    }


    /// <summary>  
    /// 获取企点账号信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="file"></param>  
    /// <returns></returns>  
    public static void QqGetQiDianInfo(this IBasicAPI basicApi, MessageContext context,
        string? file)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;qidian_get_account_info"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 设置登录号资料  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="nickname">名称</param>  
    /// <param name="company">公司</param>  
    /// <param name="email">公司</param>  
    /// <param name="college">学校</param>  
    /// <param name="personal_note">个人说明</param>  
    /// <returns></returns>  
    public static void QqSetQQProfile(this IBasicAPI basicApi, MessageContext context,
        string? nickname, string? company, string? email, string? college, string? personal_note)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["nickname"] = nickname;
        json["company"] = company;
        json["email"] = email;
        json["college"] = college;
        json["personal_note"] = personal_note;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_qq_profile"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取陌生人信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id">QQ 号</param>  
    /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时, 但响应更快）</param>  
    /// <returns></returns>  
    public static void QqGetStrangeInfo(this IBasicAPI basicApi, MessageContext context,
        string? user_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_stranger_info"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取好友列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <returns></returns>  
    public static void QqGetFriendList(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_friend_list"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取单向好友列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <returns></returns>  
    public static void QqGetUnidirectionalFriendList(this IBasicAPI basicApi, MessageContext context)
    {
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_unidirectional_friend_list"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 删除好友  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id">好友 QQ 号</param>  
    /// <returns></returns>  
    public static void QqDeleteFrined(this IBasicAPI basicApi, MessageContext context,
        string? user_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;delete_friend"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取群信息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时, 但响应更快）</param>  
    /// <returns></returns>  
    public static void QqGetGroupInfo(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_info"
        };
        basicApi.Action(response);
    }

    public static void QqGetGroupMember(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? user_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["user_id"] = user_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_member_info"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取群成员列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id"></param>  
    /// <param name="user_id"></param>  
    /// <param name="no_cache"></param>  
    /// <returns></returns>  
    public static void QqGetGroupMemberList(this IBasicAPI basicApi, MessageContext context,
        string? group_id, bool? no_cache)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["no_cache"] = no_cache == null ? "" : no_cache.ToString();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_group_member_list"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 图片 OCR/// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="image"></param>  
    /// <returns></returns>  
    public static void QqOcrImage(this IBasicAPI basicApi, MessageContext context,
        string? image)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["image"] = image;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;ocr_image"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取群系统消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id"></param>  
    /// <returns></returns>  
    public static void QqGetGroupSystemMessage(this IBasicAPI basicApi, MessageContext context)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = new object(),
            ResponseRoute = "qq;get_group_system_msg"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 上传私聊文件  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="user_id"></param>  
    /// <param name="file">本地文件路径</param>  
    /// <param name="name">文件名称</param>  
    /// <returns></returns>  
    public static void QqUploadPrivateMessage(this IBasicAPI basicApi, MessageContext context,
        string? user_id, string? file, string? name)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["user_id"] = user_id;
        json["file"] = file;
        json["name"] = name;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;upload_private_file"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 上传群文件  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id"><群号/param>  
    /// <param name="file">本地文件路径</param>  
    /// <param name="name">储存名称</param>  
    /// <param name="folder">父目录ID</param>  
    /// <returns></returns>  
    public static void QqUploadGroupFile(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? file, string? name, string? folder)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["file"] = file;
        json["name"] = name;
        json["folder"] = folder;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;upload_group_file"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 发送群公告  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <param name="content">群号</param>  
    /// <param name="image">图片路径（可选）</param>  
    /// <returns></returns>  
    public static void QqSendGroupNotice(this IBasicAPI basicApi, MessageContext context,
        string? group_id, string? content, string? image)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        json["content"] = content;
        json["image"] = image;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;_send_group_notice"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取群公告  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <returns></returns>  
    public static void QqGetGroupNotice(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;group_id"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 设置精华消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息ID</param>  
    /// <returns></returns>  
    public static void QqSetEssenceMessage(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;set_essence_msg"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 移出精华消息  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="message_id">消息ID</param>  
    /// <returns></returns>  
    public static void QqRemoveEssenceMessage(this IBasicAPI basicApi, MessageContext context,
        string? message_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["message_id"] = message_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;message_id"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 获取精华消息列表  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="group_id">群号</param>  
    /// <returns></returns>  
    public static void QqGetImage(this IBasicAPI basicApi, MessageContext context,
        string? group_id)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["group_id"] = group_id;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;get_essence_msg_list"
        };
        basicApi.Action(response);
    }

    /// <summary>  
    /// 检查链接安全性  
    /// </summary>  
    /// <param name="basicApi"></param>  
    /// <param name="context"></param>  
    /// <param name="url">需要检查的链接</param>  
    /// <returns>安全等级, 1: 安全 2: 未知 3: 危险</returns>  
    public static void QqCheckLinkSafety(this IBasicAPI basicApi, MessageContext context,
        string? url)
    {
        Dictionary<string, string?> json = new Dictionary<string, string?>();
        json["url"] = url;
        ResponseContext response = new()
        {
            Message = context,
            TargetPlatfromJson = json,
            ResponseRoute = "qq;url"
        };
        basicApi.Action(response);
    }
}