using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;

/// <summary>
/// 负责对 QQ 的 UnderProperty 提供逻辑化的解析
/// </summary>
public static class QqMessageExtension
{
    private static string? QqGetSender(this MessageContext messageContext, string prop)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject[prop] == null ? null : jObject[prop]!.ToString();
    }

    public static string? QqGetSenderNick(this MessageContext messageContext)
        => messageContext.QqGetSender("nickname");

    public static string? QqGetUserId(this MessageContext messageContext)
        => messageContext.QqGetSender("user_id");

    public static string? QqGetSex(this MessageContext messageContext)
        => messageContext.QqGetSender("sex");

    public static string? QqGetAge(this MessageContext messageContext)
        => messageContext.QqGetSender("age");

    public static string? QqGetTempGroup(this MessageContext messageContext)
        => messageContext.QqGetSender("group_id");


    public static string? QqGetGroupInCard(this MessageContext messageContext)
        => messageContext.QqGetSender("card");


    public static string? QqGetGroupInArea(this MessageContext messageContext)
        => messageContext.QqGetSender("area");


    public static string? QqGetGroupInLevel(this MessageContext messageContext)
        => messageContext.QqGetSender("level");

    public static string? QqGetGroupInRole(this MessageContext messageContext)
        => messageContext.QqGetSender("role");

    public static string? QqGetGroupInTitle(this MessageContext messageContext)
        => messageContext.QqGetSender("title");

    private static string? TryGetValue(this MessageContext messageContext, string key)
    {
        if (messageContext.UnderProperty.TryGetValue(key, out string? value) && !string.IsNullOrEmpty(value))
            return value;
        return null;
    }

    public static string? QqGetMessageFont(this MessageContext messageContext)
        => messageContext.TryGetValue("font");

    public static string? QqGetTempPrivateMessageSource(this MessageContext messageContext)
        => messageContext.TryGetValue("temp_source");

    public static bool QqIsGroupMessageAnonymous(this MessageContext messageContext)
    {
        return messageContext.UnderProperty.TryGetValue("anonymous", out string? value) && !string.IsNullOrEmpty(value);
    }

    private static string? QqGetAnony(this MessageContext messageContext, string prop)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["anonymous"]);
        return jObject[prop] == null ? null : jObject[prop]!.ToString();
    }

    public static string? QqGetAnonymousId(this MessageContext messageContext)
        => messageContext.QqGetAnony("id");

    public static string? QqGetAnonymousName(this MessageContext messageContext)
        => messageContext.QqGetAnony("name");

    public static string? QqGetAnonymousFlag(this MessageContext messageContext)
        => messageContext.QqGetAnony("flag");

    public static string? QqGetMessageId(this MessageContext messageContext)
        => messageContext.TryGetValue("message_id");

    public static string? QqGetPostType(this MessageContext messageContext)
        => messageContext.TryGetValue("post_type");

    public static string? QqGetNoticeType(this MessageContext messageContext)
        => messageContext.TryGetValue("notice_type");

    public static string? QqGetMessageType(this MessageContext messageContext)
        => messageContext.TryGetValue("message_type");

    private static string? QqGetFile(this MessageContext messageContext, string prop)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject[prop] == null ? null : jObject[prop]!.ToString();
    }

    public static string? QqGetFileId(this MessageContext messageContext)
        => messageContext.QqGetFile("id");

    public static string? QqGetFileName(this MessageContext messageContext)
        => messageContext.QqGetFile("name");

    public static string? QqGetFileSize(this MessageContext messageContext)
        => messageContext.QqGetFile("size");

    public static string? QqGetFileBusid(this MessageContext messageContext)
        => messageContext.QqGetFile("busid");

    public static string? QqGetBanMessageDuration(this MessageContext messageContext)
        => messageContext.TryGetValue("duration");

    public static string? QqGetHonorType(this MessageContext messageContext)
        => messageContext.TryGetValue("honor_type");

    public static string? QqGetTitleChangingNewTitle(this MessageContext messageContext)
        => messageContext.TryGetValue("title");

    public static string? QqGetCardChangingOldCard(this MessageContext messageContext)
        => messageContext.TryGetValue("card_old");

    public static string? QqGetCardChangingNewCard(this MessageContext messageContext)
        => messageContext.TryGetValue("card_new");

    public static string? QqGetFileUrl(this MessageContext messageContext)
        => messageContext.QqGetFile("url");

    public static string? QqGetFriendOrGroupAddComment(this MessageContext messageContext)
        => messageContext.TryGetValue("comment");

    public static string? QqGetFriendOrGroupAddFlag(this MessageContext messageContext)
        => messageContext.TryGetValue("flag");

    public static string? QqGetRequestType(this MessageContext messageContext)
        => messageContext.TryGetValue("request_type");

    private static string DealWithSpecialCode(string code)
    {
        return code.Replace("&", "&amp;")
            .Replace("[", "&#91;")
            .Replace("]", "&#93;")
            .Replace(",", "&#44;");
    }

    public static string QqCreateFace(string faceid)
    {
        string arg = DealWithSpecialCode(faceid);
        return "[CQ:face,id=" + arg + "]";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file">语音文件名</param>
    /// <param name="magic">发送时可选, 默认 0, 设置为 1 表示变声</param>
    /// <param name="url">语音 URL</param>
    /// <param name="cache">只在通过网络 URL 发送时有效, 表示是否使用已缓存的文件, 默认 1</param>
    /// <param name="proxy">只在通过网络 URL 发送时有效, 表示是否通过代理下载文件 ( 需通过环境变量或配置文件配置代理 ) , 默认 1</param>
    /// <param name="timeout">	只在通过网络 URL 发送时有效, 单位秒, 表示下载网络文件的超时时间 , 默认不超时</param>
    /// <returns></returns>
    public static string QqCreateRecord(string? file, bool? magic, string? url, bool? cache, bool? proxy,
        string? timeout)
    {
        StringBuilder arg = new StringBuilder();
        if (file != null)
        {
            arg.Append(",file=" + DealWithSpecialCode(file));
        }

        if (magic != null)
        {
            arg.Append(",magic=" + (magic.Value ? "1" : "0"));
        }

        if (url != null)
        {
            arg.Append(",url=" + DealWithSpecialCode(url));
        }

        if (cache != null)
        {
            arg.Append(",cache=" + (cache.Value ? "1" : "0"));
        }

        if (proxy != null)
        {
            arg.Append(",proxy=" + (proxy.Value ? "1" : "0"));
        }

        if (timeout != null)
        {
            arg.Append(",timeout=" + DealWithSpecialCode(timeout));
        }

        return "[CQ:record" + arg + "]";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file">视频地址, 支持http和file发送</param>
    /// <param name="cover">视频封面, 支持http, file和base64发送, 格式必须为jpg</param>
    /// <param name="c">2或者为3，通过网络下载视频时的线程数, 默认单线程. (在资源不支持并发时会自动处理)</param>
    /// <returns></returns>
    public static string QqCreateShortVideo(string? file, string? cover, int? c)
    {
        StringBuilder arg = new StringBuilder();
        if (file != null)
        {
            arg.Append(",file=" + DealWithSpecialCode(file));
        }

        if (cover != null)
        {
            arg.Append(",cover=" + DealWithSpecialCode(cover));
        }

        if (c != null)
        {
            arg.Append(",c=" + c);
        }

        return "[CQ:video" + arg + "]";
    }

    public static string QqCreateAtAll()
    {
        return "[CQ:at,qq=all]";
    }

    public static string QqCreateAtSomebody(string qq, string? name)
    {
        var args = DealWithSpecialCode(qq);
        if (name != null)
        {
            args = args + ",name=" + DealWithSpecialCode(name);
        }

        return "[CQ:at,qq=" + args + "]";
    }

    /// <summary>
    /// 音乐分享
    /// </summary>
    /// <param name="type">qq 163 xm分别表示使用 QQ 音乐、网易云音乐、虾米音乐</param>
    /// <param name="id">歌曲 ID</param>
    /// <returns></returns>
    public static string QqCreateShareMusic(string type, string id)
    {
        var args = DealWithSpecialCode(type) + ",id=" + DealWithSpecialCode(id);
        return "[CQ:music,type=" + args + "]";
    }

    /// <summary>
    /// 音乐自定义分享
    /// </summary>
    /// <param name="type">为custom,表示音乐自定义分享</param>
    /// <param name="url">点击后跳转目标 URL</param>
    /// <param name="audio">点击后跳转目标 URL</param>
    /// <param name="title">标题</param>
    /// <param name="content">发送时可选, 内容描述</param>
    /// <param name="image">发送时可选, 图片 URL</param>
    /// <returns></returns>
    public static string QqCreateShareDiyMusic(string type, string url, string audio, string title, string? content,
        string? image)
    {
        var args = DealWithSpecialCode(type) + ",url=" + DealWithSpecialCode(url) +
                   ",audio=" + DealWithSpecialCode(audio) + ",title=" + title;
        if (content != null)
        {
            args = args + ",content=" + content;
        }

        if (image != null)
        {
            args = args + ",image=" + image;
        }

        return "[CQ:music,type=" + args + "]";
    }

    /// <summary>
    /// 创建自定义图片
    /// </summary>
    /// <param name="file">	图片文件名</param>
    /// <param name="type">flash闪照 && show 秀图 默认普通图片</param>
    /// <param name="subType">0	正常图片;1	表情包, 在客户端会被分类到表情包图片并缩放显示;2	热图;3	斗图;4	智图?;7	贴图;8	自拍;9	贴图广告?;10	有待测试;13	热搜图</param>
    /// <param name="url">图片 URL</param>
    /// <param name="cache">只在通过网络 URL 发送时有效, 表示是否使用已缓存的文件, 默认 true</param>
    /// <param name="id">秀图时的特效id, 默认为40000.40000	普通;40001	幻影;40002	抖动;40003	生日;40004	爱你;40005	征友</param>
    /// <param name="c">通过网络下载图片时的线程数, 默认单线程. (在资源不支持并发时会自动处理)</param>
    /// <returns></returns>
    public static string QqCreatePicture(string? file, string? type, string? subType, string? url, bool? cache,
        string? id, string? c)
    {
        StringBuilder arg = new StringBuilder();
        arg.AppendNotNullArgs("file", file);
        arg.AppendNotNullArgs("type", type);
        arg.AppendNotNullArgs("subType", subType);
        arg.AppendNotNullArgs("url", url);
        var cacheS = cache == null ? null : cache.Value ? "1" : "0";
        arg.AppendNotNullArgs("cache", cacheS, false);
        arg.AppendNotNullArgs("id", id);
        arg.AppendNotNullArgs("c", c);

        return "[CQ:image" + arg + "]";
    }

    /// <summary>
    /// 创建回复文本
    /// </summary>
    /// <param name="id">回复时所引用的消息id, 必须为本群消息.</param>
    /// <param name="text">自定义回复的信息</param>
    /// <param name="qq">自定义回复时的自定义QQ, 如果使用自定义信息必须指定.</param>
    /// <param name="time">	自定义回复时的时间, 格式为Unix时间</param>
    /// <param name="seq">起始消息序号</param>
    /// <returns></returns>
    public static string QqCreateReply(string id, string? text, string? qq, string? time, string? seq)
    {
        var args = new StringBuilder();
        args.Append(DealWithSpecialCode(id));
        args.AppendNotNullArgs("text", text, false);
        args.AppendNotNullArgs("qq", qq, false);
        args.AppendNotNullArgs("time", time, false);
        args.AppendNotNullArgs("seq", seq, false);

        return "[CQ:reply,id=" + args + "]";
    }

    /// <summary>
    /// 戳一戳
    /// </summary>
    /// <param name="qq"></param>
    /// <returns></returns>
    public static string QqCreatePoke(string qq)
    {
        var args = DealWithSpecialCode(qq);
        return "[CQ:poke,qq=" + args + "]";
    }

    /// <summary>
    /// 文本转语音
    /// </summary>
    /// <param name="text">内容</param>
    /// <returns></returns>
    public static string QqCreateTextIntoRecord(string text)
    {
        var args = DealWithSpecialCode(text);
        return "[CQ:tts,text=" + args + "]";
    }

    /// <summary>
    /// 发送XML消息
    /// </summary>
    /// <param name="data">xml内容, xml中的value部分, 记得实体化处理</param>
    /// <param name="resid"></param>
    /// <returns></returns>
    public static string QqCreateXmlMessage(string data, string? resid)
    {
        var args = DealWithSpecialCode(data);

        if (resid != null) args += ",resid=" + resid;

        return "[CQ:xml,data=" + args + "]";
    }

    /// <summary>
    /// 发送Json消息，且根据下面的字符串进行转义
    /// ","=> &#44;
    ///"&"=> &amp;
    ///"["=> &#91;
    ///"]"=> &#93;
    /// </summary>
    /// <param name="data">json内容, json的所有字符串记得实体化处理</param>
    /// <param name="resid">默认不填为0, 走小程序通道, 填了走富文本通道发送</param>
    /// <returns></returns>
    public static string QqCreateJsonMessage(string data, string? resid)
    {
        var args = DealWithSpecialCode(data);

        if (resid != null) args += ",resid=" + resid;

        return "[CQ:xml,data=" + args + "]";
    }

    private static void AppendNotNullArgs(this StringBuilder arg, string name, string? value, bool specialCode = true)
    {
        if (value == null) return;
        arg.Append("," + name + "=" + (specialCode ? DealWithSpecialCode(value) : value));
    }

    /// <summary>
    /// 一种xml的图片消息（装逼大图）
    /// </summary>
    /// <param name="file">和image的file字段对齐, 支持也是一样的</param>
    /// <param name="minwidth">默认不填为400, 最小width</param>
    /// <param name="minheight">默认不填为400, 最小height</param>
    /// <param name="maxwidth">	默认不填为500, 最大width</param>
    /// <param name="maxheight">默认不填为1000, 最大height</param>
    /// <param name="source">分享来源的名称, 可以留空</param>
    /// <param name="icon">分享来源的icon图标url, 可以留空</param>
    /// <returns></returns>
    public static string QqCreateCardImage(string file, string? minwidth, string? minheight, string? maxwidth,
        string? maxheight, string? source, string? icon)
    {
        StringBuilder arg = new StringBuilder();
        arg.Append(",file=" + DealWithSpecialCode(file));

        arg.AppendNotNullArgs("minwidth", minwidth);
        arg.AppendNotNullArgs("minheight", minheight);
        arg.AppendNotNullArgs("maxwidth", maxwidth);
        arg.AppendNotNullArgs("maxheight", maxheight);
        arg.AppendNotNullArgs("source", source);
        arg.AppendNotNullArgs("icon", icon);

        return "[CQ:cardimage" + arg + "]";
    }
}