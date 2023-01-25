using Microsoft.Extensions.Hosting;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.Core.Kernel.Builder
{
    /// <summary>
    /// Work类相当于框架 Libs 自身的后台类，本服务是否注册进入框架类可以根据用户自己的喜好判断
    /// 根据 SoruxBot v1.0-Beta 的变更，框架自身的 Work 已经不会默认注册到后台服务，转而使用 Shell 的 Work 类
    /// </summary>
    public class Work : BackgroundService
    {
        private readonly ILoggerService _loggerService;
        private readonly Bot _bot;

        public Work(ILoggerService loggerService, Bot bot)
        {
            _loggerService = loggerService;
            _bot = bot;
            loggerService.Info("BackGroundWorkService", "Init background service.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //后台服务
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}