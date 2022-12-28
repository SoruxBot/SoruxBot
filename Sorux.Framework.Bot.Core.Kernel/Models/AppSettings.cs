using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Models
{
    public static class AppSettings
    {
        //基本信息，此处的信息我也不知道在哪个地方写入配置里面
        //如果哪一天有人审计代码看到了这个地方，麻烦你补一下，谢谢
        public const string FrameworkVersion = "1.0.0-Aplha";

        public const string CoreKernelVersion = "1.0.0-Beta";

        public const string WebDirector = "https://soruxbot.sorux.cn";

        public const string WebApiDirector = "https://api.sorux.cn/soruxbot/v1";
        
    }
}
