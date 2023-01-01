using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;


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
                _loggerService.Error("PluginsRegister","The plugin:" + name + "can not be loaded exactly" +
                                                      ", please check the plugin with its developer." +
                                                      "ErrorCode:EX0001");
                return;
            }
            
            IBasicInformationRegister? basicInformationRegister = Activator.CreateInstance(type) as IBasicInformationRegister;
            if (basicInformationRegister == null)
            {
                _loggerService.Error("PluginsRegister","The plugin:" + name + "can not be loaded exactly" +
                                                       ", please check the plugin with its developer." +
                                                       "ErrorCode:EX0002");
                return;
            }
            JsonConfig jsonfile;
            try
            {
                jsonfile = JsonConvert.DeserializeObject<JsonConfig>(
                    File.ReadAllText(DsLocalStorage.GetPluginsConfigDirectory() + "\\" + name.Replace(".dll", ".json")));
            }
            catch (Exception e)
            {
                _loggerService.Error("PluginsRegister","The plugin:" + name + " loses json file:"+name.Replace(".dll", ".json")  +
                                                       "ErrorCode:EX0003");
                return;
            }
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
