using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

public interface IBasicAPI
{
    /// <summary>
    /// 发送私聊消息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="content"></param>
    public void SendPrivateMessage(MessageContext context,string content);
    
    
}