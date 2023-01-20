

namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    /// <summary>
    /// 热重启支持
    /// </summary>
    public interface IPluginsStoragePermanentAble
    {
        void StoreLocal();
        void RemoveStore();
        void RestoreFromLocal();
    }
}
