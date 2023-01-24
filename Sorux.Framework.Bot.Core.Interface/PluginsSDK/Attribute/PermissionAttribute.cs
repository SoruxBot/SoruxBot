namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// Provide command carry out permission
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : System.Attribute
    {
        public string PermissionNode { get; init; }

        public PermissionAttribute(string node)
        {
            this.PermissionNode = node;
        }
    }
}