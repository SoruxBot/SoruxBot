using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models
{
    public class JsonConfig
    {
        string Name;
        int Privilege;
        PermissionNodeConfig Permission;
        string PermissionDeniedMsg;
    }

    public class PermissionNodeConfig 
    {
        string Node;
        string Group;
    }
}
