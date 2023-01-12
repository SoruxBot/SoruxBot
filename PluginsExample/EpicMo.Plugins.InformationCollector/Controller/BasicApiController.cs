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

public class BasicApiController : BotController
{
    private ILoggerService _loggerService;
    private IBasicAPI _bot;
    private RestClient restClient;
    public BasicApiController(ILoggerService loggerService,IBasicAPI bot)
    {
        this._loggerService = loggerService;
        this._bot = bot;
        restClient = new RestClient("https://api.szfx.top");
    }
    
    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"today")]
    public PluginFucFlag TodayGet(MessageContext context)
    {
        _bot.SendGroupMessage(context, QqMessageExtension
                .QqCreatePicture("https://api.szfx.top/morning-paper/?type=full",null,"0",null,false,"40000",null));
        return PluginFucFlag.MsgIntercepted;
    }

    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"qrcodetext [msg]")]
    public PluginFucFlag QrcodeMsgyGet(MessageContext context,string msg)
    {
        _bot.SendGroupMessage(context, QqMessageExtension
            .QqCreatePicture("https://api.szfx.top/qrcode/?size=100&text=" + msg,null,"0",null,false,"40000",null));
        return PluginFucFlag.MsgIntercepted;
    }
    
    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"qrcodeurl [url]")]
    public PluginFucFlag QrcodeUrlGet(MessageContext context,string url)
    {
        _bot.SendGroupMessage(context, QqMessageExtension
            .QqCreatePicture("https://api.szfx.top/qrcode/?size=100&url="+url,null,"0",null,false,"40000",null));
        return PluginFucFlag.MsgIntercepted;
    }

    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"infourl [url]")]
    public PluginFucFlag UrlGet(MessageContext context,string url)
    {
        RestRequest request = new RestRequest("/url/?");
        request.AddQueryParameter("url",url);
        var res = restClient.Execute(request);
        _bot.SendGroupMessage(context, res.Content!);
        return PluginFucFlag.MsgIntercepted;
    }
    
    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"infoip [ip]")]
    public PluginFucFlag IpGet(MessageContext context,string ip)
    {
        RestRequest request = new RestRequest("/ip/?");
        request.AddQueryParameter("ip",ip);
        var res = restClient.Execute(request);
        _bot.SendGroupMessage(context, res.Content!);
        return PluginFucFlag.MsgIntercepted;
    }
    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"music163 [name]")]
    public PluginFucFlag Music163Get(MessageContext context,string name)
    {
        RestRequest request = new RestRequest("/music163/?");
        request.AddQueryParameter("song",name);
        var res = restClient.Execute(request);
        Music163 music163 = JsonConvert.DeserializeObject<Music163>(res.Content!)!;
        _bot.SendGroupMessage(context,"找到歌曲Id:"+music163.id+"\n歌手:"+music163.singer+"名称："+music163.song);
        var client = new RestClient(music163.url.Replace("\\",""));
        string path;
        if (System.OperatingSystem.IsWindows())
        {
            path = Directory.GetCurrentDirectory() + "\\gocq\\data\\voices\\" + music163.song.GetSha256();
        }
        else
        {
            path = Directory.GetCurrentDirectory() + "/gocq/data/voices/" + music163.song.GetSha256();
        }
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }
        using (var writer = File.OpenWrite(path))
        {
            var req = new RestRequest("")
            {
                ResponseWriter = responseStream =>
                {
                    using (responseStream)
                    {
                        responseStream.CopyTo(writer);
                    }
                    return null;
                }
            };
            var response = client.DownloadData(req);
        }

        Directory.GetCurrentDirectory();
        _bot.SendGroupMessage(context,
            QqMessageExtension.QqCreateRecord(music163.song.GetSha256(),null,null,null,null,null));
        _bot.SendGroupMessage(context, QqMessageExtension
                .QqCreatePicture(music163.img.Replace("\\",""),null,"0",null,false,"40000",null));
        return PluginFucFlag.MsgIntercepted;
    }
}