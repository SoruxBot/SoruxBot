namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
/// <summary>
/// 用于标记一些特殊的框架支持的标记符
/// </summary>
public static class AltFrameworkTag
{
    //SF的含义为： SoruxFramework
    
    /// <summary>
    /// 若 Command 的值为这个，那么表示一切条件都会触发
    /// </summary>
    public static string AnyTriggerAction = "[SF:AnyAction]";

    /// <summary>
    /// 表示使用正则匹配对 Command 触发条件进行匹配，用法为此标识 + 正则表达式字符串形式 来作为 Command 的参数
    /// </summary>
    public static string RegexTriggerAction = "[SF:Regex]";
    
    
}