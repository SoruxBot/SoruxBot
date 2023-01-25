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
            if (File.Exists(AppendCurrentDirectory("parse.lock"))) return;

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
            => AppendCurrentDirectory("Config", "MessageQueue.sb");

        public static string GetPluginsDataDirectory()
            => AppendCurrentDirectory("Plugins", "Data");

        public static string GetPluginsDataDirectory(string name)
            => AppendCurrentDirectory("Plugins", "Data", name);

        public static string GetPluginsDataFile(string name, string key)
            => AppendCurrentDirectory("Plugins", "Data", name, key + ".bin");

        public static string GetPluginsConfigDirectory()
            => AppendCurrentDirectory("Plugins", "Config");

        public static string GetPluginsDirectory()
            => AppendCurrentDirectory("Plugins", "Bin");

        public static string GetCurrentPath()
            => Directory.GetCurrentDirectory();

        public static string GetPluginsSqliteDataPath()
            => AppendCurrentDirectory("Lib", "pluginsData.bin");

        public static string GetPluginsPermissionDataPath()
            => AppendCurrentDirectory("Lib", "pluginsPermission.bin");

        public static string GetResponseQueuePath()
            => AppendCurrentDirectory("Config", "ResponseQueue.sb");
    }
}