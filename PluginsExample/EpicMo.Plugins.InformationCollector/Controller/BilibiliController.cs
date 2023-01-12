using EpicMo.Plugins.InformationCollector.Models;
using Newtonsoft.Json;
using RestSharp;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace EpicMo.Plugins.InformationCollector.Controller;

public class BilibiliController : BotController
{
    private ILoggerService _loggerService;
    private IBasicAPI _bot;
    private RestClient restClient;
    public BilibiliController(ILoggerService loggerService,IBasicAPI bot)
    {
        this._loggerService = loggerService;
        this._bot = bot;
        restClient = new RestClient("https://api.szfx.top");
    }

    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.None, "[SF-ALL]")]
    public PluginFucFlag BilibiliGet(MessageContext context,string content)
    {
        if (content.Contains("bilibili.com"))
        {
            RestRequest restRequest = new RestRequest("/bilibili/api.php?");
            int index = content.IndexOf("bilibili.com/video/");
            if (index == -1)
                return PluginFucFlag.MsgPassed;
            index = index + "bilibili.com/video/".Length;
            string id;
            if (content.Substring(index,2).ToLower().Equals("bv"))
            {
                id = content.Substring(index, 12);//Bid是10位
            }
            else
            {
                id = content.Substring(index, 11);
            }
            string url = "https://www.bilibili.com/video/" + id;
            restRequest.AddQueryParameter("url", url);
            var result = restClient.Execute(restRequest);
            Bilibili bilibili = JsonConvert.DeserializeObject<Bilibili>(result.Content!)!;
            if (bilibili.code.Equals("200"))
            {
                _bot.SendGroupMessage(context,
                    QqMessageExtension.QqCreateReply(context.UnderProperty["message_id"]
                        ,null,null,null,null)
                    + "视频标题：" + bilibili.title + "\nUp主名称:" + bilibili.upname + "\n描述：" + bilibili.desc +
                    "\n视频Bid" + bilibili.bvid + "\nAid:" + bilibili.aid
                    +QqMessageExtension.QqCreatePicture(bilibili.cover,null,"0",null,false,"40000",null));
            }
            return PluginFucFlag.MsgIntercepted;
        }
        else if(content.Contains("b23.tv"))
        {
            RestRequest restRequest = new RestRequest("/bilibili/api.php?");
            int index;
            string id;
            if (content.Contains("b23.tv\\/"))
            {
                index = content.IndexOf("b23.tv\\/");
                if (index == -1)
                    return PluginFucFlag.MsgPassed;
                index = index + "b23.tv\\/".Length;
                id = content.Substring(index, 7);//Bv是7位
            }else if (content.Contains("b23.tv/"))
            {
                index = content.IndexOf("b23.tv/");
                if (index == -1)
                    return PluginFucFlag.MsgPassed;
                index = index + "b23.tv/".Length;
                id = content.Substring(index, 7);//Bv是7位
            }
            else
            {
                return PluginFucFlag.MsgPassed;
            }
            string url = "https://b23.tv/" + id;
            restRequest.AddQueryParameter("url", url);
            var result = restClient.Execute(restRequest);
            Bilibili bilibili = JsonConvert.DeserializeObject<Bilibili>(result.Content!)!;
            if (bilibili.code.Equals("200"))
            {
                _bot.SendGroupMessage(context,
                    QqMessageExtension.QqCreateReply(context.UnderProperty["message_id"]
                        ,null,null,null,null)
                    + "视频标题：《" + bilibili.title + "》\nUp主名称:\"" + bilibili.upname + "\"\n描述：" + bilibili.desc +
                    "\n视频Bid:" + bilibili.bvid + ";Aid:" + bilibili.aid
                    +QqMessageExtension.QqCreatePicture(bilibili.cover,null,"0",null,false,"40000",null));
            }
            return PluginFucFlag.MsgIntercepted;
        }
        return PluginFucFlag.MsgPassed;
    }
    
    public static string TextGainCenter(string left, string right, string text) {
        //判断是否为null或者是empty
        if (string.IsNullOrEmpty(left))
            return "";
        if (string.IsNullOrEmpty(right))
            return "";
        if (string.IsNullOrEmpty(text))
            return "";

        int Lindex = text.IndexOf(left); //搜索left的位置
            
        if (Lindex == -1){ //判断是否找到left
            return "";
        }
        //abcd a d
        Lindex = Lindex + left.Length; //取出left右边文本起始位置
            
        int Rindex = text.IndexOf(right, Lindex);//从left的右边开始寻找right
           
        if (Rindex == -1){//判断是否找到right
            return "";   
        }
            
        return text.Substring(Lindex, Rindex - Lindex);//返回查找到的文本
    }
}