using RestSharp;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace EpicMo.Plugins.InformationCollector.Controller;

public class HelpController : BotController
{
    private ILoggerService _loggerService;
    private IBasicAPI _bot;
    public HelpController(ILoggerService loggerService,IBasicAPI bot)
    {
        this._loggerService = loggerService;
        this._bot = bot;
    }
    
    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"help [msg]")]
    public PluginFucFlag HelpGet(MessageContext context,string msg)
    {
        if (msg.Equals("ic"))
        {
            _bot.SendGroupMessage(context
                ,"这是信息收集(informationcollector，简称ic)插件，用于整理一些信息和辅助用于使用!\n" 
                 +"Bilibili功能：被动使用，当检测到Bilibili链接时会自动回复链接的详细消息\n"
                 +"Github功能，被动使用，当检测到Github链接时会自动回复链接的详细信\n"
                 +"EveryDay功能，主动使用，通过'#today'来使用，获取当天的相关消息\n"
                 +"YiYan功能，主动使用，通过'#saying <type>'来使用，其中type可选，可为a-f的字母\n"
                 +"Ip查询，主动使用，通过'#infoip [ip]'来使用，获取详细信息\n"
                 +"Url查询，主动使用，通过'#infourl [url]'来使用，获取详细信息\n"
                 +"Qrcode功能，主动使用，通过'#qrcodetext [msg],#qrcodeurl [url]'来使用，制作一个二维码\n"
                 +"歌曲查询，主动使用，通过'#music163 [name]'来查询歌曲\n");
        }
        return PluginFucFlag.MsgIntercepted;
    }

}