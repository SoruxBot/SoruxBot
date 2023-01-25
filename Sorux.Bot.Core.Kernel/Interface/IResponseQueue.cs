using Sorux.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Bot.Core.Kernel.Interface;

/// <summary>
/// 输出队列
/// </summary>
public interface IResponseQueue
{
    public void SetNextReponse(ResponseContext context);
    public ResponseContext? GetNextResponse();
    public void RestoreFromLocalStorage();
    public void SaveIntoLocalStorage();
    public void DisposeFromLocalStorage();
}