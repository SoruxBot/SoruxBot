using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;

namespace Sorux.Framework.Bot.Core.Kernel.Models;

public class LongMessageContext
{
    //用于存储消息对象
    public Dictionary<string, MessageContext?> MessageContexts { get; set; }
    
}