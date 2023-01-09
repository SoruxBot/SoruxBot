using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Ability;
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
            object intance = Activator.CreateInstance(type)!;
            IBasicInformationRegister? basicInformationRegister = intance as IBasicInformationRegister;
            if (basicInformationRegister == null)
            {
                _loggerService.Error("PluginsRegister","The plugin:" + name + "can not be loaded exactly" +
                                                       ", please check the plugin with its developer." +
                                                       "ErrorCode:EX0002");
                return;
            }
            JsonConfig? jsonfile;
            try
            {
                switch (_botContext.ServiceProvider.GetService<IConfiguration>()!["ContextRuntimeSystem"])
                {
                    case "Windows":
                        jsonfile = JsonConvert.DeserializeObject<JsonConfig>(
                            File.ReadAllText(DsLocalStorage.GetPluginsConfigDirectory() + "\\" + name.Replace(".dll", ".json")));
                        break;
                    case "Linux":
                    case "MacOS":
                        jsonfile = JsonConvert.DeserializeObject<JsonConfig>(
                            File.ReadAllText(DsLocalStorage.GetPluginsConfigDirectory() + "/" + name.Replace(".dll", ".json")));
                        break;
                    default:
                        _loggerService.Fatal("PluginsRegister","The system kind is not known! Exit...");
                        return;
                }
                
            }
            catch (Exception e)
            {
                _loggerService.Error("PluginsRegister","The plugin:" + name + " loses json file:"+name.Replace(".dll", ".json")  +
                                                       "ErrorCode:EX0003");
                return;
            }
            IPluginsStorage pluginsStorage = _botContext.ServiceProvider.GetRequiredService<IPluginsStorage>();
            //判断privilege是否合法：
            IConfiguration configuration = _botContext.ServiceProvider
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
            
            //效验插件的 DLL 信息是否正确，以防止篡改
            if (!basicInformationRegister.GetDLL().Equals(name))
            {
                _loggerService.Warn("PluginsRegister", "Plugins Information Error for plugin:" + name + "\nYou " +
                                                       "maybe using unsafe plugin,the SoruxBot will not load it.");
                return;
            }
            pluginsStorage.AddPlugin(name,
                                     basicInformationRegister.GetAuthor(),
                                     path,
                                     basicInformationRegister.GetVersion(),
                                     basicInformationRegister.GetDescription(),
                                     newPrivilege);
            
            _loggerService.Info("PluginsRegister","Catch plugin:" + name);
            
            //Register 注册可选特性
            Type[] types = type.GetInterfaces();
            pluginsStorage.SetPluginInfor(name, "PluginsUUIDRegister", "false");
            pluginsStorage.SetPluginInfor(name, "CommandPermission", "false");
            pluginsStorage.SetPluginInfor(name, "CommandPrefix", "false");
            foreach (var subInterface in types)
            {
                if (subInterface == typeof(IPluginsUUIDRegister))
                {
                    pluginsStorage.SetPluginInfor(name, "PluginsUUIDRegister", "true");
                    IPluginsUUIDRegister uuid = intance as IPluginsUUIDRegister;
                    pluginsStorage.SetPluginInfor(name, "UUID", uuid.GetUUID());
                }
                
                if (subInterface == typeof(ICommandPermission))
                {
                    pluginsStorage.SetPluginInfor(name, "CommandPermission", "true");
                    ICommandPermission permission = intance as ICommandPermission;
                    pluginsStorage.SetPluginInfor(name, "PermissionDeniedAutoAt",
                        permission.IsPermissionDeniedAutoAt().ToString());
                    pluginsStorage.SetPluginInfor(name, "PermissionDeniedAutoReply",
                        permission.IsPermissionDeniedAutoReply().ToString());
                    pluginsStorage.SetPluginInfor(name, "PermissionDeniedLeakOut",
                        permission.IsPermissionDeniedLeakOut().ToString());
                    pluginsStorage.SetPluginInfor(name, "PermissionDeniedMessage",
                        permission.GetPermissionDeniedMessage());
                }
                
                if (subInterface == typeof(ICommandPrefix))
                {
                    pluginsStorage.SetPluginInfor(name, "CommandPrefix", "true");
                    ICommandPrefix prefix = intance as ICommandPrefix;
                    pluginsStorage.SetPluginInfor(name, "CommandPrefixContent", prefix.GetCommandPrefix());
                }
            }
        }

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public void RegisterRoute(string name, string path)
        {
            _botContext.ServiceProvider.GetRequiredService<PluginsDispatcher>().RegisterCommandRoute(path,name);
        }
    }
}
