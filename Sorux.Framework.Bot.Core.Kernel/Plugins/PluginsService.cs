using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins
{
    public class PluginsService
    {
        public static void RegisterPlugins()
        {
            new DirectoryInfo(DsLocalStorage.GetPluginsDirectory())
                    .GetFiles()
                    .ToList()
                    .ForEach(plugin => PluginsRegister.Register(plugin.FullName,plugin.Name));
        }


    }
}
