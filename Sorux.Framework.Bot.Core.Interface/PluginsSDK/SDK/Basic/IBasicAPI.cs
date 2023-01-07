using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

public interface IBasicAPI
{
    /// <summary>
    /// 向框架的回复队列发送自定义的 ResponseContext
    /// </summary>
    /// <param name="context"></param>
    /// <param name="response"></param>
    public void Action(ResponseContext response);
    /// <summary>
    /// 以异步方式向框架的动作请求队列发送自定义的 ResponseContext，并期待回复且回复为 string 的 JSON 文本
    /// </summary>
    /// <param name="context"></param>
    /// <param name="response"></param>
    public Task<string> ActionAsync(ResponseContext response);
    /// <summary>
    /// 以同步方式向框架的回复队列发送自定义的 ResponseContext，并期待回复且回复为 string 的 JSON 文本
    /// </summary>
    /// <param name="context"></param>
    /// <param name="response"></param>
    public string ActionCompute(ResponseContext response);
    /// <summary>
    /// 发送私聊消息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="content"></param>
    public void SendPrivateMessage(MessageContext context,string content);
    /// <summary>
    /// 发送群聊消息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="content"></param>
    public void SendGroupMessage(MessageContext context, string content);
    /// <summary>
    /// 发送群聊消息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="content"></param>
    public Task<string> SendGroupMessageAsync(MessageContext context, string content);
    /// <summary>
    /// 发送群聊消息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="content"></param>
    public string SendGroupMessageCompute(MessageContext context, string content);
    /// <summary>
    /// 异步发送私聊消息，并且得到返回数据
    /// </summary>
    /// <param name="context"></param>
    /// <param name="content"></param>
    /// <returns>MessageId</returns>
    public Task<string> SendPrivateMessageAsync(MessageContext context,string content);
    /// <summary>
    /// 同步发送私聊消息，并且得到返回数据
    /// </summary>
    /// <param name="context"></param>
    /// <param name="content"></param>
    /// <returns>MessageId</returns>
    public string SendPrivateMessageCompute(MessageContext context,string content);
    
    
}