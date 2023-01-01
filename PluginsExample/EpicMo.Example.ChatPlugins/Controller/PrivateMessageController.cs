using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace EpicMo.Example.ChatPlugins.Controller;

public class PrivateMessageController : BotController
{
    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.None,"SayHello")]
    public void SayHello()
    {
        
    }

    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.None, "SayHello [uuid] <optional>")]
    public void SayHello(string uuid,string optional)
    {
        
    }
}