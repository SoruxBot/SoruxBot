
namespace Sorux.Framework.Bot.Core.Kernel.DataStorage
{
    /// <summary>
    /// 内置的低级信息存储类
    /// </summary>
    public class DsLocalStorage
    {
        static DsLocalStorage()
        {
            // 操作系统的特判
            if (System.OperatingSystem.IsWindows())
            {
                if (!new FileInfo(Directory.GetCurrentDirectory() + "\\parse.lock").Exists)
                {
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Config").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Logs").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Plugins").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Plugins\\Data").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Plugins\\Bin").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Plugins\\Config").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Lib").Create();
                    new FileInfo(Directory.GetCurrentDirectory() + "\\parse.lock").Create();
                    new FileInfo(Directory.GetCurrentDirectory() + "\\Config\\AppSettings.json").Create();
                }
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                if (!new FileInfo(Directory.GetCurrentDirectory() + "/parse.lock").Exists)
                {
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "/Config").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "/Logs").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "/Plugins").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "/Plugins/Data").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "/Plugins/Bin").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "/Plugins/Config").Create();
                    new DirectoryInfo(Directory.GetCurrentDirectory() + "/Lib").Create();
                    new FileInfo(Directory.GetCurrentDirectory() + "/parse.lock").Create();
                    new FileInfo(Directory.GetCurrentDirectory() + "/Config/AppSettings.json").Create();
                }
            }
            
        }

        public static string GetMessageQueuePath()
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Config\\MessageQueue.sb";
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Config/MessageQueue.sb";
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Config/MessageQueue.sb";
            }
        }

        public static string GetPluginsDataDirectory()
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Plugins\\Data";
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Data";
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Data";
            }
        }

        public static string GetPluginsDataDirectory(string name)
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Plugins\\Data\\" + name;
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Data/" + name;
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Data/" + name;
            }
        }
        
        public static string GetPluginsDataFile(string name,string key)
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Plugins\\Data\\" + name +"\\" + key + ".bin";
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Data/" + name +"/" +key + ".bin";
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Data/" + name + "/" +key + ".bin";
            }
        }
        
        public static string GetPluginsConfigDirectory()
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Plugins\\Config";
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Config";
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Config";
            }
        }

        public static string GetPluginsDirectory()
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Plugins\\Bin";
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Bin";
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Plugins/Bin";
            }
        }

        public static string GetCurrentPath()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetPluginsSqliteDataPath()
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Lib\\pluginsData.bin";
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Lib/pluginsData.bin";
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Lib/pluginsData.bin";
            }
        }
        
        public static string GetResponseQueuePath()
        {
            if (System.OperatingSystem.IsWindows())
            {
                return Directory.GetCurrentDirectory() + "\\Config\\ResponseQueue.sb";
            }else if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                return Directory.GetCurrentDirectory() + "/Config/ResponseQueue.sb";
            }
            else
            {
                return Directory.GetCurrentDirectory() + "/Config/ResponseQueue.sb";
            }
        }

    }
}
