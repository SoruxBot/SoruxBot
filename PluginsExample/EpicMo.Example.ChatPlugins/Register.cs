using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;

namespace EpicMo.Example.ChatPlugins
{
    public class Register : IBasicInformationRegister , ICommandPrefix , ICommandPermission , IPluginsUUIDRegister
    {
        public string GetAuthor() => "EpicMo"; //表示插件的作者信息
        public string GetName() => "SoruxBot示例插件"; //表示插件的名称，这个名称会被框架的日志记录

        public string GetDLL() => "EpicMo.Example.ChatPlugins.dll"; //必须对应插件编译后的 DLL 名称

        public string GetDescription() => "这是一个C#语境下的示例插件，请勿在正式群聊中启用本插件以防止打扰到群成员。" +
            "本插件不含任何有用的功能，但是会尽量详细地使用框架的特性，以及C#原生语境下的各个框架优化功能。" +
            "为方便理解，本插件注释采用全中文。"; //插件的描述信息
        public string GetVersion() => "1.0.0-Aplha"; //插件的版本，必须遵循 [主版本号].[次版本号].[补丁号]-[非发行版内部阿拉伯编号]
        
        //用于插件中心托管的预置 UUID
        public string GetUUID() => "b36e2e39-88c6-4ff7-99cb-df393f0aba69"; //插件对应 UUID 编号
        
        //Prefix -> 实现了 Prefix 接口才可以使用插件单独定制的 Prefix
        public string GetCommandPrefix() => "#"; //插件的触发头，必须为一个字符，且仅仅在 Prefix.Single 时被启用
        public string GetPermissionDeniedMessage() => "您并不含有权限节点相应的执行权限."; //用户执行时因为权限被拒绝而发送的消息
        public bool IsPermissionDeniedAutoAt() => true; //权限不够时是否会艾特用户
        public bool IsPermissionDeniedLeakOut() => true; //权限不够时是否通知用户对应的权限节点是什么
        public bool IsPermissionDeniedAutoReply() => true; //权限不够时是否自动反馈权限不够的提示
    }
}