using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Sorux.Framework.Bot.Core.Kernel.Models;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Interface;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins
{
    internal class PluginsRegister
    {
        private BotContext _botContext;
        private ILoggerService _loggerService;
        public PluginsRegister(BotContext context, ILoggerService loggerService)
        {
            this._botContext = context;
            this._loggerService = loggerService;
        }
        
        public void Register(string path,string name)
        {
            Assembly assembly = Assembly.LoadFile(path);
            Type? type = assembly.GetType(name.Replace(".dll", ".Register"));//命名空间规定为Register
            if (type == null)
            {
                _loggerService.Warn("PluginsRegister","The plugin:" + name + "can not be loaded exactly" +
                                                      ", please check the plugin with its developer");
                return;
            }
            
            IBasicInformationRegister? basicInformationRegister = Activator.CreateInstance(type) as IBasicInformationRegister;
            if (basicInformationRegister == null)
            {
                _loggerService.Warn("PluginsRegister","The plugin:" + name + "can not be loaded exactly" +
                                                      ", please check the plugin with its developer");
                return;
            }
            JsonConfig jsonfile = JsonConvert.DeserializeObject<JsonConfig>(
                File.ReadAllText(DsLocalStorage.GetPluginsConfigDirectory() + "\\" + name.Replace(".dll", ".json")));
            IPluginsStorage pluginsStorage = _botContext.GetProvider().GetRequiredService<IPluginsStorage>();
            //判断privilege是否合法：
            IConfiguration configuration = _botContext.GetProvider()
                                                      .GetRequiredService<IBot>()
                                                      .Configuration
                                                      .GetRequiredSection("PluginsDispatcher")
                                                      .GetRequiredSection("PrivilegeSchedule");
            int privilege = jsonfile.Privilege;
            int newPrivilege = jsonfile.Privilege;
            switch (configuration["PushStandard"])
            {
                case "Lower":
                    if (!pluginsStorage.TryGetPrivilege(privilege,out newPrivilege))
                    {
                        _loggerService.Warn("PluginsRegister","Privilege Conflict for " + privilege 
                            + ", push for " + newPrivilege);
                    }
                    break;
                case "Upper":
                    if (!pluginsStorage.TryGetPrivilegeUpper(privilege,out newPrivilege))
                    {
                        _loggerService.Warn("PluginsRegister","Privilege Conflict for " + privilege 
                            + ", push for " + newPrivilege);
                    }
                    break;
                default:
                    _loggerService.Warn("PluginsRegister","Config Section:PushStandard Error,for its config:" + configuration["PushStandard"]);
                    break;
            }
            
            pluginsStorage.AddPlugin(name,
                                     basicInformationRegister.GetAuthor(),
                                     basicInformationRegister.GetDLL(),
                                     basicInformationRegister.GetVersion(),
                                     basicInformationRegister.GetDescription(),
                                     newPrivilege);
        }
    }
}
