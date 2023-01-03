using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// The method marked this attribute will enable its long communicate fuction.
    /// The framework will provide some fuction to waiting for user's next input.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LongCommunicateAttribute : System.Attribute
    {
        public LongCommunicateAttribute() { }
        //The default communicate mark will permit all user to input.
        //If you need to lmit user please use CommandPermission and mark LongCommunicatePermissionBinding
        public enum LongCommunicateType
        {
            None = 0, //【保留值】
            SingleUserListener = 1, //对单个用户进行监听，这个用户指触发长消息模块中 MessageContext 里面的 TriggerId
            AllUserListener = 2, //对所有用户进行监听
        }
    }
}
