using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;

public static class QqMessageExtension
{
    public static string GetSenderNick(this MessageContext messageContext) => messageContext.UnderProperty["sender.nickname"];
}