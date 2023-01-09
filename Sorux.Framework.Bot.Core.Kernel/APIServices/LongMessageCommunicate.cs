using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

namespace Sorux.Framework.Bot.Core.Kernel.APIServices;

public class LongMessageCommunicate : ILongMessageCommunicate
{

    public Task<MessageContext> ReadNextPrivateMessageAsync(MessageContext context)
    {
        throw new NotImplementedException();
    }

    public Task<MessageContext> ReadNextGroupMessageAsync(LongCommunicateType type, MessageContext context)
    {
        throw new NotImplementedException();
    }

    public Task<MessageContext> CreateGenericListenerAsync(EventType eventType, string? targetPlatform, string? targetAction, Func<MessageContext, bool> action,
        bool isIntercept, PluginFucFlag? flag)
    {
        throw new NotImplementedException();
    }
}