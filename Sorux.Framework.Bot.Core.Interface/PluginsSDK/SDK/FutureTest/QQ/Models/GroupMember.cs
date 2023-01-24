namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ.Models;

public class GroupMember
{
    public string group_id { get; set; }
    public string user_id { get; set; }
    public string nickname { get; set; }

    /// <summary>
    /// 群名片
    /// </summary>
    public string card { get; set; }

    public string sex { get; set; }
    public string age { get; set; }
    public string area { get; set; }

    /// <summary>
    /// 加群时间戳
    /// </summary>
    public string join_time { get; set; }

    /// <summary>
    /// 最后发言时间戳
    /// </summary>
    public string last_sent_time { get; set; }

    /// <summary>
    /// 成员等级
    /// </summary>
    public string level { get; set; }

    /// <summary>
    /// 角色, owner 或 admin 或 member
    /// </summary>
    public string role { get; set; }

    /// <summary>
    /// 是否不良记录成员
    /// </summary>
    public string unfriendly { get; set; }

    /// <summary>
    /// 专属头衔
    /// </summary>
    public string title { get; set; }

    /// <summary>
    /// 专属头衔过期时间戳
    /// </summary>
    public string title_expire_time { get; set; }

    /// <summary>
    /// 是否允许修改群名片
    /// </summary>
    public string card_changeable { get; set; }

    /// <summary>
    /// 禁言到期时间
    /// </summary>
    public string shut_up_timestamp { get; set; }
}