namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
/// <summary>
/// 表示在执行消息方法后执行相应的命令
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AfterMethodAttribute: System.Attribute
{
    public AfterMethodAttribute()
    {
        
    }
}