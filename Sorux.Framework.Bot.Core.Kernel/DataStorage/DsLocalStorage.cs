namespace Sorux.Framework.Bot.Core.Kernel.DataStorage
{
    /// <summary>
    /// 内置的低级信息存储类
    /// </summary>
    public class DsLocalStorage
    {
        private static string AppendCurrentDirectory(params string[] subfolder)
        {
            var l = new List<string>();
            l.Add(Directory.GetCurrentDirectory());
            l.AddRange(subfolder);
            return Path.Join(l.ToArray());
        }

        static DsLocalStorage()
        {
            if (new FileInfo(AppendCurrentDirectory("parse.lock")).Exists) return;

            Directory.CreateDirectory(AppendCurrentDirectory("Config"));
            Directory.CreateDirectory(AppendCurrentDirectory("Logs"));
            Directory.CreateDirectory(AppendCurrentDirectory("Plugins"));
            Directory.CreateDirectory(AppendCurrentDirectory("Plugins", "Data"));
            Directory.CreateDirectory(AppendCurrentDirectory("Plugins", "Bin"));
            Directory.CreateDirectory(AppendCurrentDirectory("Plugins", "Config"));
            Directory.CreateDirectory(AppendCurrentDirectory("Lib"));
            File.Create(AppendCurrentDirectory("parse.lock"));
            File.Create(AppendCurrentDirectory("Config", "AppSettings.json"));
        }

        public static string GetMessageQueuePath()
        {
            return AppendCurrentDirectory("Config", "MessageQueue.sb");
        }

        public static string GetPluginsDataDirectory()
        {
            return AppendCurrentDirectory("Plugins", "Data");
        }

        public static string GetPluginsDataDirectory(string name)
        {
            return AppendCurrentDirectory("Plugins", "Data", name);
        }

        public static string GetPluginsDataFile(string name, string key)
        {
            return AppendCurrentDirectory("Plugins", "Data", name, key + ".bin");
        }

        public static string GetPluginsConfigDirectory()
        {
            return AppendCurrentDirectory("Plugins", "Config");
        }

        public static string GetPluginsDirectory()
        {
            return AppendCurrentDirectory("Plugins", "Bin");
        }

        public static string GetCurrentPath()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetPluginsSqliteDataPath()
        {
            return AppendCurrentDirectory("Lib", "pluginsData.bin");
        }

        public static string GetPluginsPermissionDataPath()
        {
            return AppendCurrentDirectory("Lib", "pluginsPermission.bin");
        }

        public static string GetResponseQueuePath()
        {
            return AppendCurrentDirectory("Config", "ResponseQueue.sb");
        }
    }
}