using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace EpicMo.Example.ChatPlugins;

public class BeforeMethodAction : IBeforeMethodAction
{
    public void Before(MessageContext context, ILoggerService loggerService)
    {
        loggerService.Info(new Register().GetName(),"插件已经处理了此消息："+context.Message.GetRawMessage());
    }
}