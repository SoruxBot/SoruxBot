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
    public static string? GetSenderNick(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["nickname"] == null ? null : jObject["nickname"]!.ToString();
    }
    
    public static string? GetUserId(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["user_id"] == null ? null : jObject["user_id"]!.ToString();
    }
    
    public static string? GetSex(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["sex"] == null ? null : jObject["sex"]!.ToString();
    }
    public static string? GetAge(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["age"] == null ? null : jObject["age"]!.ToString();
    }
    public static string? GetTempGroup(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["group_id"] == null ? null : jObject["group_id"]!.ToString();
    }
    public static string? GetGroupInCard(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["card"] == null ? null : jObject["card"]!.ToString();
    }
    public static string? GetGroupInArea(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["area"] == null ? null : jObject["area"]!.ToString();
    }

    public static string? GetGroupInLevel(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["level"] == null ? null : jObject["level"]!.ToString();
    }
    
    public static string? GetGroupInRole(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["role"] == null ? null : jObject["role"]!.ToString();
    }
    
    public static string? GetGroupInTitle(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["sender"]);
        return jObject["title"] == null ? null : jObject["title"]!.ToString();
    }
    
    public static string? GetMessageFont(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("font",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetTempPrivateMessageSource(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("temp_source",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static bool isGroupMessageAnonymous(this MessageContext messageContext)
    {
        return messageContext.UnderProperty.TryGetValue("anonymous", out string? value) && !value.Equals("");
    }
    
    public static string? GetAnoymousId(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["anonymous"]);
        return jObject["id"] == null ? null : jObject["id"]!.ToString();
    }
    
    public static string? GetAnoymousName(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["anonymous"]);
        return jObject["name"] == null ? null : jObject["name"]!.ToString();
    }
    
    public static string? GetAnoymousFlag(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["anonymous"]);
        return jObject["flag"] == null ? null : jObject["flag"]!.ToString();
    }
    
    public static string? GetMessageId(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("message_id",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetPostType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("post_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetNoticeType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("notice_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetMessageType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("message_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetFileId(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["id"] == null ? null : jObject["id"]!.ToString();
    }
    
    public static string? GetFileName(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["name"] == null ? null : jObject["name"]!.ToString();
    }
    
    public static string? GetFileSize(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["size"] == null ? null : jObject["size"]!.ToString();
    }
    
    public static string? GetFileBusid(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["busid"] == null ? null : jObject["busid"]!.ToString();
    }
    
    public static string? GetBanMessageDuration(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("duration",out string? value) && !value.Equals(""))
            return value;
        return null;
    }

    public static string? GetHonorType(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("honor_type",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetTitleChangingNewTitle(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("title",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetCardChangingOldCard(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("card_old",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetCardChangingNewCard(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("card_new",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetFileUrl(this MessageContext messageContext)
    {
        JObject jObject = JObject.Parse(messageContext.UnderProperty["file"]);
        return jObject["url"] == null ? null : jObject["url"]!.ToString();
    }
    public static string? GetFriendOrGroupAddComment(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("comment",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetFriendOrGroupAddFlag(this MessageContext messageContext)
    {
        if (messageContext.UnderProperty.TryGetValue("flag",out string? value) && !value.Equals(""))
            return value;
        return null;
    }
    
    public static string? GetRequestType(this MessageContext messageContext)
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

    public static string CreateFace(string faceid)
    {
        string arg = DealWithSpecialCode(faceid);
        return "[CQ:face,id=" + arg + "]";
    }
    
    public static string CreateRecord(string? file,bool? magic,string? url,bool? cache,bool? proxy,string? timeout)
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
    
    public static string CreateShortVideo(string? file,string? cover,int? c)
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

    public static string AtAll()
    {
        return "[CQ:at,qq=all]";
    }

    public static string AtSomebody(string qq, string? name)
    {
        var args = DealWithSpecialCode(qq);
        if (name != null)
        {
            args = args + ",name=" + DealWithSpecialCode(name);
        }
        return "[CQ:at,qq=" + args + "]";
    }
    
    public static string ShareMusic(string type, string id)
    {
        var args = DealWithSpecialCode(type) + ",id=" + DealWithSpecialCode(id);
        return "[CQ:music,type=" + args + "]";
    }
    
    public static string ShareDiyMusic(string type, string url,string audio,string title,string? content,string? image)
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
    
    
}