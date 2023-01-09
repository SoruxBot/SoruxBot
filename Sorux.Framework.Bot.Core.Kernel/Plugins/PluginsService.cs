using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.APIServices;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
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
            
            PluginsRegister pluginsRegister = _botContext.ServiceProvider.GetRequiredService<PluginsRegister>();
            new DirectoryInfo(DsLocalStorage.GetPluginsDirectory())
                    .GetFiles()
                    .ToList()
                    .ForEach(plugin => pluginsRegister.Register(plugin.FullName,plugin.Name));
            
            _botContext.ServiceProvider.GetRequiredService<IPluginsStorage>()
                       .GetPluginsListByPrivilege().ForEach(sp =>
                       {
                           pluginsRegister.RegisterRoute(sp.name,sp.filepath);
                       });
        }
        /// <summary>
        /// 配置插件服务，在Shell不启用插件的时候不进行加载，以防止向容器注入插件服务，反而导致错误在同游其他地方的子模块报错
        /// </summary>
        /// <param name="context"></param>
        public static void ConfigurePluginsServices(IServiceCollection services)
        {
            //注册基础服务
            services.AddSingleton<PluginsService>();
            services.AddSingleton<PluginsRegister>();
            services.AddSingleton<IPluginsStorage, PluginsLocalStorage>();
            services.AddSingleton<PluginsDispatcher>();
            services.AddSingleton<PluginsCommandLexer>();
            services.AddSingleton<PluginsHost>();
            //添加API服务
            services.AddSingleton<IBasicAPI, BasicApi>();
            services.AddSingleton<ILongMessageCommunicate, LongMessageCommunicate>();
        }
    }
}
