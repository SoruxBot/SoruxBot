namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;

/// <summary>
/// 表示约束代码逻辑
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class PlatformConstraintAttribute : System.Attribute
{
    public PlatformConstraintAttribute(string platform)
    {
        
    }

    public PlatformConstraintAttribute(string platform, string action)
    {
        
    }
}