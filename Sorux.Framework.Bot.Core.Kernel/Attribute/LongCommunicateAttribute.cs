using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Attribute
{
    /// <summary>
    /// The method marked this attribute will enable its long communicate fuction.
    /// The framework will provide some fuction to waiting for user's next input.
    /// </summary>
    public class LongCommunicateAttribute : System.Attribute
    {
        public LongCommunicateAttribute() { }
        //The default communicate mark will permit all user to input.
        //If you need to lmit user please use CommandPermission and mark LongCommunicatePermissionBinding
        public enum LongCommunicateType
        {
            SingleUserListener = 1,
            AllUserListener = 2,
            PermissionBinding = 3,
            CoolDownTimeBinding = 4,
            PermissionBindingAndCoolDownTimeBinding = 5,
        }
    }
}
