using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Models
{
    public partial class AppSettings
    {
        //Information
        public const string Frameworkversion = "1.0.0-Aplha";

        public const string CoreKernelVersion = "1.0.0-Aplha";

        public const string WebDirector = "https://soruxbot.sorux.cn";

        public const string WebApiDirector = "https://api.sorux.cn/soruxbot/v1";

        public static string MessageQueueType = "MqDictionary";

        public static string DataStorageType  = "DsLocalStorage";

    }
}
