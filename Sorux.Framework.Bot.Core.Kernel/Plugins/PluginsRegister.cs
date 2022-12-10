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
            Type? type = assembly.GetType(name.Replace(".dll", ".RegisterInformation"));
            if (type == null)
                Global.GetGlobal().Logger
                      .LogWarn("[SoruxBot][PluginsRegister]The plugin:"+name+"can not be loaded exactly" +
                      ", please check the plugin with its developer");

        }
    }
}
