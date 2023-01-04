
namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    public interface IMessageQueue
    {
        //得到队列中的 Message
        public string? GetNextMessageRequest();
        //向队列中放入Message
        public void SetNextMsg(string value);
        //存储临时信息
        public void RestoreFromLocalStorage();
        public void SaveIntoLocalStorage();
        public void DisposeFromLocalStorage();
    }
}
