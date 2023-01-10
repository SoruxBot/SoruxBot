using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models;

public class PluginsPermissionDescriptor
{
    public string PermissionNode { get; set; }
    
    public Func<MessageContext,string> FilterAction { get; set; }

    public string Description { get; set; }
    
}