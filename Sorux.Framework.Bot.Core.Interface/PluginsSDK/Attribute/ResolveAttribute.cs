using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// The method mark with this attribute will receive resovled data.
    /// e.g. [CQ:at 11101...] will be replaced by 11101  
    /// </summary>
    public class ResolveAttribute : System.Attribute
    {
        public ResolveAttribute() { }
    }
}
