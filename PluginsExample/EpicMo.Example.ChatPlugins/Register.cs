using Sorux.Framework.Bot.Core.Kernel.Plugins.Interface;

namespace EpicMo.Example.ChatPlugins
{
    public class Register : IBasicInformationRegister , IPluginsUUIDRegister
    {
        public string GetAuthor() => "EpicMo";
        public string GetName() => "SoruxBot示例插件";

        public string GetDLL() => "epicmo.example.chatplugins";

        public string GetDescription() => "这是一个C#语境下的示例插件，请勿在正式群聊中启用本插件以防止打扰到群成员。" +
            "本插件不含任何有用的功能，但是会尽量详细地使用框架的特性，以及C#原生语境下的各个框架优化功能。" +
            "为方便理解，本插件注释采用全中文。";

        public string GetVersion() => "1.0.0-Aplha";

        public string GetUUID() => "b36e2e39-88c6-4ff7-99cb-df393f0aba69";
    }
}