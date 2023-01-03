using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// Provide command carry out permission
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : System.Attribute
    {
        public PermissionAttribute(string node)
        {

        }
    }
}
