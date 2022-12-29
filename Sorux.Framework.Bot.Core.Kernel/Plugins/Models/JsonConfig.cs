using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models
{
    public class JsonConfig
    {
        public string Name;
        public int Privilege;
        public PermissionNodeConfig Permission;
        public string PermissionDeniedMsg;
    }

    public class PermissionNodeConfig 
    {
        string Node;
        string Group;
    }
}
