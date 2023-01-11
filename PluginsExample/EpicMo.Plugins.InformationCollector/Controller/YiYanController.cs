using EpicMo.Plugins.InformationCollector.Models;
using Newtonsoft.Json;
using RestSharp;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace EpicMo.Plugins.InformationCollector.Controller;

public class YiYanController: BotController
{
    private ILoggerService _loggerService;
    private IBasicAPI _bot;
    private RestClient _client;
    public YiYanController(ILoggerService loggerService,IBasicAPI bot,ILongMessageCommunicate longMessageCommunicate)
    {
        this._loggerService = loggerService;
        this._bot = bot;
        _client = new RestClient("https://v1.hitokoto.cn/");
    }

    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"saying <type>")]
    public PluginFucFlag YiYanGet(MessageContext context,string? type)
    {
        RestRequest request = new RestRequest();
        request.Method = Method.Get;
        if (!string.IsNullOrEmpty(type))
        {
            request.AddQueryParameter("c", type);
        }
        var result = _client.Execute(request);
        YiYan model = JsonConvert.DeserializeObject<YiYan>(result.Content!)!;
        if (!string.IsNullOrEmpty(model.from_who))
        {
            _bot.SendGroupMessage(context,model.hitokoto + "   ---" + model.from_who);
        }
        else
        {
            _bot.SendGroupMessage(context,model.hitokoto);
        }
        return PluginFucFlag.MsgIntercepted;
    }

}