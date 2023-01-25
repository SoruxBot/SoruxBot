namespace Sorux.Bot.Core.Interface.PluginsSDK.Attribute;

/// <summary>
/// 表示约束代码逻辑
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class PlatformConstraintAttribute : System.Attribute
{
    public string PlatformConstraint { get; init; }

    public PlatformConstraintAttribute(string platform)
    {
        this.PlatformConstraint = platform;
    }

    public PlatformConstraintAttribute(string platform, string action)
    {
        this.PlatformConstraint = platform + ";" + action;
    }
}