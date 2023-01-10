using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;

namespace EpicMo.Plugins.InformationCollector;

public class Register: IBasicInformationRegister , ICommandPrefix
{
    public string GetAuthor() => "EpicMo";

    public string GetDescription() => "用于信息的手机、整理";

    public string GetName() => "InformationCollector";

    public string GetVersion() => "1.0.0-release";

    public string GetDLL() => "EpicMo.Plugins.InformationCollector.dll";

    public string GetCommandPrefix() => "#";
}