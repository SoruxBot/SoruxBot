using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Sorux.Framework.Bot.Core.Kernel.Models;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Interface;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins
{
    internal static class PluginsRegister
    {
        public static void Register(string path,string name)
        {
            Assembly assembly= Assembly.LoadFile(path);
            Type? type = assembly.GetType(name.Replace(".dll", ".Register"));
            if (type == null)
            {
                Global.GetGlobal().Logger
                    .LogWarn("[SoruxBot][PluginsRegister]The plugin:" + name + "can not be loaded exactly" +
                             ", please check the plugin with its developer");
                return;
            }
                IBasicInformationRegister? basicInformationRegister = Activator.CreateInstance(type) as IBasicInformationRegister;
            if (basicInformationRegister == null)
            {
                Global.GetGlobal().Logger
                      .LogWarn("[SoruxBot][PluginsRegister]The plugin:" + name + "can not be loaded exactly" +
                      ", please check the plugin with its developer");
                return;
            }
            JsonConfig jsonfile = JsonConvert.DeserializeObject<JsonConfig>(
                File.ReadAllText(DsLocalStorage.GetPluginsConfigDirectory() + "\\" + name.Replace(".dll", ".json")));
            Global.GetGlobal().pluginsStorage
                                   .AddPlugins(basicInformationRegister.GetName(),
                                               basicInformationRegister.GetAuthor(),
                                               basicInformationRegister.GetDLL(),
                                               basicInformationRegister.GetVersion(),
                                               basicInformationRegister.GetDescription()
                                               );

           
                
        }
    }
}
