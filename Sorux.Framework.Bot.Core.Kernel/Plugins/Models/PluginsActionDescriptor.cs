namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
/// <summary>
/// 插件描述类别，用于描述插件 Action 的具体信息
/// </summary>
public class PluginsActionDescriptor
{
    /// <summary>
    /// Action对应的参数
    /// </summary>
    public List<PluginsActionParameter> ActionParameters { get; set; } = new List<PluginsActionParameter>();
    
    public bool IsParameterBinded { get; set; }
    
    public Delegate ActionDelegate { get; set; }
    
    public string InstanceTypeName { get; set; }
    
    public PluginsPermissionDescriptor? PluginsPermissionDescriptor { get; set; }

    public bool IsParameterLexerDisable { get; set; } = false;
}