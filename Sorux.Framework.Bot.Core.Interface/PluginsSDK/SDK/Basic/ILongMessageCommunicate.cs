using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

public interface ILongMessageCommunicate
{
    /// <summary>
    /// 等待下一个输入的消息
    /// </summary>
    /// <param name="type">输入消息对象的类型</param>
    /// <returns></returns>
    public MessageContext ReadNextMessage(LongCommunicateAttribute.LongCommunicateType type 
                                            = LongCommunicateAttribute.LongCommunicateType.SingleUserListener);
    
}
