using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Kernel.Interface;

/// <summary>
/// 输出队列
/// </summary>
public interface IResponseQueue
{
    public void SetNextReponse(MessageContext context);
    public MessageContext? GetNextResponse(MessageContext context);
    public void RestoreFromLocalStorage();
    public void SaveIntoLocalStorage();
    public void DisposeFromLocalStorage();
}