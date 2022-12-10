using Sorux.Framework.Bot.Core.Kernel.Filter;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.MessageQueue;
using Sorux.Framework.Bot.Core.Kernel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage
{
    public class PluginsLocalStorage : IPluginsStorage
    {
        private static readonly PluginsLocalStorage pluginsLocalStorage = new PluginsLocalStorage();
        private PluginsLocalStorage() 
        {
            DataTable pluginsList = new DataTable();
            
        }
        private DataSet pluginsDB = new DataSet();
        public static PluginsLocalStorage GetInstance() => pluginsLocalStorage;

        public bool AddPlugins(string name, string author, string filename, string version, string description)
        {
            
        }

        public bool AddPlugins(string name, string author, string filename, string version, string description, string uuid)
        {
            throw new NotImplementedException();
        }

        public string GetEventAction(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public List<string> GetEventWaitingList(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllPlugins()
        {
            throw new NotImplementedException();
        }

        public void RemovePlugins(string name)
        {
            throw new NotImplementedException();
        }
    }
}
