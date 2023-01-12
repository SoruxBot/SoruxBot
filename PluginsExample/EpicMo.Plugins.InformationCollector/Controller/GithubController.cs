using RestSharp;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace EpicMo.Plugins.InformationCollector.Controller;

public class GithubController: BotController
{
    private ILoggerService _loggerService;
    private IBasicAPI _bot;
    public GithubController(ILoggerService loggerService,IBasicAPI bot)
    {
        this._loggerService = loggerService;
        this._bot = bot;
    }

    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.None, "[SF-ALL]")]
    public PluginFucFlag GithubGet(MessageContext context,string content)
    {
        if (content.Contains("github.com"))
        {
            RestClient restClient = new RestClient(content);
            RestRequest restRequest = new RestRequest("",Method.Get);
            restRequest.AddHeader("accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            var result = restClient.Execute(restRequest);
            var url = TextGainCenter("meta property=\"og:image\" content=\"", "\" /><meta property=\"og:image:alt",
                result.Content!);
            _bot.SendGroupMessage(context,
                QqMessageExtension.QqCreateReply(context.UnderProperty["message_id"]
                    ,null,null,null,null)+QqMessageExtension
                .QqCreatePicture(url,null,"0",null,false,"40000",null));
            return PluginFucFlag.MsgIntercepted;
        }
        else
        {
            return PluginFucFlag.MsgPassed;
        }
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
            
        Lindex = Lindex + left.Length; //取出left右边文本起始位置
            
        int Rindex = text.IndexOf(right, Lindex);//从left的右边开始寻找right
           
        if (Rindex == -1){//判断是否找到right
            return "";   
        }
            
        return text.Substring(Lindex, Rindex - Lindex);//返回查找到的文本
    }
}