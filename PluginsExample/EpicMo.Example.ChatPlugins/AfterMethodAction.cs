using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace EpicMo.Example.ChatPlugins;

internal class AfterMethodAction: IAfterMethodAction
{
    public void After(MessageContext context, ILoggerService loggerService)
    {
        loggerService.Info(new Register().GetName(),"插件准备处理消息："+context.Message.GetRawMessage());
    }
}