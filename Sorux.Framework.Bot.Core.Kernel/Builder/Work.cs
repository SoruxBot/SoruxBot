using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sorux.Framework.Bot.Core.Kernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    internal class Work : BackgroundService
    {
        private readonly ILoggerService _loggerService;
        private readonly Bot _bot;
        public Work(ILoggerService loggerService,Bot bot)
        {
            _loggerService= loggerService;
            _bot= bot;
            loggerService.Info("BackGroundWorkService","机器人后台服务初始化完毕");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
