namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models;

public class PermissionNode
{
    
    /// <summary>
    /// 插件的节点
    /// </summary>
    public string Node { get; set; }
    /// <summary>
    /// 权限的描述
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 权限的使用字节点
    /// </summary>
    public string ConditionType { get; set; }
    /// <summary>
    /// 权限的字符
    /// </summary>
    public string ConditionChar { get; set; }
}