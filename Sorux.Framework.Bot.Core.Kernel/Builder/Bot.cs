using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sorux.Framework.Bot.Core.Kernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public class Bot : IBot
    {
        public Bot(BotServicesFactory services,IConfiguration configuration) 
        {
            this.Services = services;
            this.Configuration = configuration;
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(this);
                    services.AddSingleton<ILoggerService,LoggerService>();
                    services.AddHostedService<Work>();
                })
                .Build();
        }
        public BotServicesFactory Services { get; init; }

        public IConfiguration Configuration { get; init; }

        private IHost _host;

        public void Dispose()
        {
            this.Stop();
        }

        public void Start()
        {
            _host.Run();
        }

        public void Stop()
        {
            _host.StopAsync();
        }
    }
}
