using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins
{
    public class PluginsService
    {
        private BotContext _botContext;
        private ILoggerService _loggerService;
        public PluginsService(BotContext context, ILoggerService loggerService)
        {
            this._botContext = context;
            this._loggerService = loggerService;
        }
        
        public void RegisterPlugins()
        {
            _loggerService.Info("PluginsRegister","Built-in Plugins Services: version:1.0.0");
            new DirectoryInfo(DsLocalStorage.GetPluginsDirectory())
                    .GetFiles()
                    .ToList()
                    .ForEach(plugin => _botContext.GetProvider()
                                                         .GetRequiredService<PluginsRegister>()
                                                         .Register(plugin.FullName,plugin.Name));
        }

        public static void ConfigurePluginsServices(BotContext context)
        {
            context.ConfigureService(services =>
            {
                services.AddSingleton<PluginsService>();
                services.AddSingleton<PluginsRegister>();
            });
        }
    }
}
