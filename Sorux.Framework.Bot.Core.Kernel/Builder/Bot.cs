using Microsoft.Extensions.Configuration;
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
        }
        public BotServicesFactory Services { get; init; }

        public IConfiguration Configuration { get; init; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
