using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;

public interface IBeforeMethodAction
{
    public void Before(MessageContext context,ILoggerService loggerService);
}