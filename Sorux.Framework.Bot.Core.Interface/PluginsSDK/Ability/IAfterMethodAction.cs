using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;

public interface IAfterMethodAction
{
    public void After(MessageContext context,ILoggerService loggerService);
}