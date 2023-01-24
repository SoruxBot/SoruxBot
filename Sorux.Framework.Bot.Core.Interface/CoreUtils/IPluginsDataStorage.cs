namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    public interface IPluginsDataStorage
    {
        bool AddStringSettings(string pluginMark, string key, string value);
        bool RemoveStringSettings(string pluginMark, string key);
        string GetStringSettings(string pluginMark, string key);
        bool EditStringSettings(string pluginMark, string key, string value);
        bool AddBinarySettings(string pluginMark, string key, byte[] value);
        bool RemoveBinarySettings(string pluginMark, string key);
        byte[]? GetBinarySettings(string pluginMark, string key);
        bool EditBinarySettings(string pluginMark, string key, byte[] value);
    }
}