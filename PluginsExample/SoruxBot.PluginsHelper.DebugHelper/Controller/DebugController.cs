using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace SoruxBot.PluginsHelper.DebugHelper.Controller;

public class DebugController : BotController
{
    private ILoggerService _loggerService;
    private IBasicAPI bot;
    public DebugController(ILoggerService loggerService,IBasicAPI bot)
    {
        this._loggerService = loggerService;
        this.bot = bot;
    }
    
    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.None,"debug [state]")]
    public PluginFucFlag Debug(MessageContext context, string state)
    {
        switch (state)
        {
            case "on":
                bot.SendPrivateMessage(context,"Debug Mode on");
                return PluginFucFlag.MsgIntercepted;
            case "off":
                bot.SendPrivateMessage(context,"Debug Mode off");
                return PluginFucFlag.MsgIntercepted;
            default:
                bot.SendPrivateMessage(context,"Error State,only be on/off but receive:" + state);
                return PluginFucFlag.MsgIntercepted;
        }
    }
    
}