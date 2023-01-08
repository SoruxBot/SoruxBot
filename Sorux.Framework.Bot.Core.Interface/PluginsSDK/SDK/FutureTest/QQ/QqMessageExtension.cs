using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;

/// <summary>
/// 负责对 QQ 的 UnderProperty 提供逻辑化的解析
/// </summary>
public static class QqMessageExtension
{
    public static string? QqGetSenderNick(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["nickname"] == null ? null : jObject["nickname"]!.ToString();
    }
    
    public static string? QqGetUserId(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["user_id"] == null ? null : jObject["user_id"]!.ToString();
    }
    
    public static string? QqGetSex(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["sex"] == null ? null : jObject["sex"]!.ToString();
    }
    public static string? QqGetAge(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["age"] == null ? null : jObject["age"]!.ToString();
    }
    public static string? QqGetTempGroup(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["group_id"] == null ? null : jObject["group_id"]!.ToString();
    }
    public static string? QqGetGroupInCard(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["card"] == null ? null : jObject["card"]!.ToString();
    }
    public static string? QqGetGroupInArea(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["area"] == null ? null : jObject["area"]!.ToString();
    }

    public static string? QqGetGroupInLevel(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["level"] == null ? null : jObject["level"]!.ToString();
    }
    
    public static string? QqGetGroupInRole(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["role"] == null ? null : jObject["role"]!.ToString();
    }
    
    public static string? QqGetGroupInTitle(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["title"] == null ? null : jObject["title"]!.ToString();
    }
    
    public static string? QqGetMessageFont(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("font",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetTempPrivateMessageSource(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("temp_source",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static bool QqIsGroupMessageAnonymous(this MessageContext messageContext)
    {
        return messageContext.UnderProperty.TryGetValue("anonymous", out string? value) && !value.Equals("");
    }
    
    public static string? QqGetAnonymousId(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["anonymous"]);
        return jObject["id"] == null ? null : jObject["id"]!.ToString();
    }
    
    public static string? QqGetAnonymousName(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["anonymous"]);
        return jObject["name"] == null ? null : jObject["name"]!.ToString();
    }
    
    public static string? QqGetAnonymousFlag(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["anonymous"]);
        return jObject["flag"] == null ? null : jObject["flag"]!.ToString();
    }
    
    public static string? QqGetMessageId(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("message_id",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetPostType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("post_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetNoticeType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("notice_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetMessageType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("message_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetFileId(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["id"] == null ? null : jObject["id"]!.ToString();
    }
    
    public static string? QqGetFileName(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["name"] == null ? null : jObject["name"]!.ToString();
    }
    
    public static string? QqGetFileSize(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["size"] == null ? null : jObject["size"]!.ToString();
    }
    
    public static string? QqGetFileBusid(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["busid"] == null ? null : jObject["busid"]!.ToString();
    }
    
    public static string? QqGetBanMessageDuration(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("duration",out string? value) && !value.Equals(""))
            return value;
        return null;
    }

    public static string? QqGetHonorType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("honor_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetTitleChangingNewTitle(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("title",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetCardChangingOldCard(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("card_old",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetCardChangingNewCard(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("card_new",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetFileUrl(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["url"] == null ? null : jObject["url"]!.ToString();
    }
    public static string? QqGetFriendOrGroupAddComment(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("comment",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetFriendOrGroupAddFlag(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("flag",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? QqGetRequestType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("request_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }

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
    public static string QqCreateRecord(string? file,bool? magic,string? url,bool? cache,bool? proxy,string? timeout)
    {
        StringBuilder arg = new StringBuilder();
        if (file != null)
        {
            arg.Append(",file=" + DealWithSpecialCode(file));
        }
        
        if (magic != null)
        {
            arg.Append(",magic=" + (magic.Value ? "1":"0"));
        }
        
        if (url != null)
        {
            arg.Append(",url=" + DealWithSpecialCode(url));
        }
        
        if (cache != null)
        {
            arg.Append(",cache=" + (cache.Value ? "1":"0"));
        }
        
        if (proxy != null)
        {
            arg.Append(",proxy=" + (proxy.Value ? "1":"0"));
        }
        
        if (timeout != null)
        {
            arg.Append(",timeout=" + DealWithSpecialCode(timeout));
        }
        return "[CQ:recode" + arg + "]";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file">视频地址, 支持http和file发送</param>
    /// <param name="cover">视频封面, 支持http, file和base64发送, 格式必须为jpg</param>
    /// <param name="c">2或者为3，通过网络下载视频时的线程数, 默认单线程. (在资源不支持并发时会自动处理)</param>
    /// <returns></returns>
    public static string QqCreateShortVideo(string? file,string? cover,int? c)
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
    public static string QqCreateShareDiyMusic(string type, string url,string audio,string title,string? content,string? image)
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
    public static string QqCreatePicture(string? file,string? type,string? subType,string? url,bool? cache,string? id,string? c)
    {
        StringBuilder arg = new StringBuilder();
        if (file != null)
        {
            arg.Append(",file=" + DealWithSpecialCode(file));
        }
        
        if (type != null)
        {
            arg.Append(",type=" + DealWithSpecialCode(type));
        }
        if (subType != null)
        {
            arg.Append(",subType=" + DealWithSpecialCode(subType));
        }
        
        if (url != null)
        {
            arg.Append(",url=" + DealWithSpecialCode(url));
        }
        
        if (cache != null)
        {
            arg.Append(",cache=" + (cache.Value ? "1":"0"));
        }
        
        if (id != null)
        {
            arg.Append(",id=" + DealWithSpecialCode(id));
        }
        
        if (c != null)
        {
            arg.Append(",c=" + DealWithSpecialCode(c));
        }
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
    public static string QqCreateReply(string id,string? text,string? qq,string? time,string? seq)
    {
        var args = DealWithSpecialCode(id);
        
        if (text != null)
        {
            args = args + ",text=" + text;
        }

        if (qq != null)
        {
            args = args + ",qq=" + qq;
        }
        
        if (time != null)
        {
            args = args + ",time=" + time;
        }
        
        if (seq != null)
        {
            args = args + ",seq=" + seq;
        }
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
    public static string QqCreateXmlMessage(string data,string? resid)
    {
        var args = DealWithSpecialCode(data);
        if (resid != null)
        {
            args = args + ",resid=" + resid;
        }
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
    public static string QqCreateJsonMessage(string data,string? resid)
    {
        var args = DealWithSpecialCode(data);
        if (resid != null)
        {
            args = args + ",resid=" + resid;
        }
        return "[CQ:xml,data=" + args + "]";
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
    public static string QqCreateCardImage(string file,string? minwidth,string? minheight,string? maxwidth,string? maxheight,string? source,string? icon)
    {
        StringBuilder arg = new StringBuilder();
        arg.Append(",file=" + DealWithSpecialCode(file));

        if (minwidth != null)
        {
            arg.Append(",minwidth=" + DealWithSpecialCode(minwidth));
        }
        if (minheight != null)
        {
            arg.Append(",minheight=" + DealWithSpecialCode(minheight));
        }
        
        if (maxwidth != null)
        {
            arg.Append(",maxwidth=" + DealWithSpecialCode(maxwidth));
        }
        
        
        if (icon != null)
        {
            arg.Append(",icon=" + DealWithSpecialCode(icon));
        }
        
        if (maxheight != null)
        {
            arg.Append(",maxheight=" + DealWithSpecialCode(maxheight));
        }
        
        if (source != null)
        {
            arg.Append(",source=" + DealWithSpecialCode(source));
        }
        return "[CQ:cardimage" + arg + "]";
    }
}