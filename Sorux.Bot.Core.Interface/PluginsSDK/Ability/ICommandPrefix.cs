namespace Sorux.Bot.Core.Interface.PluginsSDK.Ability;

/// <summary>
/// 为插件向容器内注册触发前缀
/// </summary>
public interface ICommandPrefix
{
    /// <summary>
    /// 插件的触发头，必须为一个字符，且仅仅在 Prefix.Single 时被启用
    /// </summary>
    /// <returns></returns>
    public string GetCommandPrefix();
}