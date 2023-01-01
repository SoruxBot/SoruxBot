namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;

public enum PluginFucFlag
{
    //消息通过，表示消息虽然被处理了但是并没有被标记被处理了
    MsgPassed = 0,
    //消息标记，表示消息已经被处理过了
    MsgFlag = 1,
    //消息拦截，表示消息不会被继续传递
    MsgIntercepted = 2,
    //消息忽略，表示消息没有被处理，使用本枚举量必须遵守约定，即真的没有被处理过
    MsgIgnored = 3,
}