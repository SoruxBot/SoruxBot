namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

public enum LongCommunicateType
{
    /// <summary>
    /// 表示触发消息的个体，即监听触发Action用户的下一句话
    /// </summary>
    TriggerUser,

    /// <summary>
    /// 表示触发消息的平台，即监听触发Action群聊的用户下一句话，私聊无效
    /// </summary>
    TriggerPlatform,

    /// <summary>
    /// 表示根据可选参数进行自定义监听
    /// </summary>
    TiedInformation
}