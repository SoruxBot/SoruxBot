using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models
{
    public class JsonConfig
    {
        string name;
        int privilege;
        PermissionNodeConfig permission;
        string permission_deny_msg;
    }

    public class PermissionNodeConfig 
    {
        string node;
        string group;
    }
}
