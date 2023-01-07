using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;

namespace SoruxBot.PluginsHelper.DebugHelper;

public class Register : IBasicInformationRegister , IPluginsUUIDRegister
{
    public string GetAuthor() => "SoruxBot";

    public string GetDescription() => "SoruxBot框架调试插件";

    public string GetName() => "SoruxBotDebugHelper";

    public string GetVersion() => "1.0.0-release";

    public string GetDLL() => "soruxbot.pluginshelper.debughelper";

    public string GetUUID() => "a05a3d5b-cf33-450b-9acb-4d28cba2feff";
}