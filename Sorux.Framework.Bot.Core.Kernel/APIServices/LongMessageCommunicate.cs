using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

namespace Sorux.Framework.Bot.Core.Kernel.APIServices;

public class LongMessageCommunicate : ILongMessageCommunicate
{
    public MessageContext ReadNextMessage(
        LongCommunicateAttribute.LongCommunicateType type = LongCommunicateAttribute.LongCommunicateType.SingleUserListener)
    {
        throw new NotImplementedException();
    }
}