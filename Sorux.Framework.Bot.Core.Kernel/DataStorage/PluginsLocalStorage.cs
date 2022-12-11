using Sorux.Framework.Bot.Core.Kernel.Filter;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.MessageQueue;
using Sorux.Framework.Bot.Core.Kernel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage
{
    public class PluginsLocalStorage : IPluginsStorage
    {
        private static readonly PluginsLocalStorage pluginsLocalStorage = new PluginsLocalStorage();
        private DataSet pluginsDB = new DataSet();
        private PluginsLocalStorage() 
        {
            DataTable pluginsList = new DataTable("pluginsList");
            DataColumn[] dataColumns = {
                new DataColumn("id"),
                new DataColumn("name"),
                new DataColumn("author"),
                new DataColumn("filename"),
                new DataColumn("version"),
                new DataColumn("Description"),
                new DataColumn("uuid")
            };
            dataColumns[0].Unique = true;
            pluginsList.Columns.AddRange(dataColumns);
            pluginsDB.Tables.Add(pluginsList);
        }
        
        public static PluginsLocalStorage GetInstance() => pluginsLocalStorage;

        public bool AddPlugins(string name, string author, string filename, string version, string description)
        {
            DataRow dataRow = pluginsDB.Tables["pluginsList"].NewRow();
            dataRow["name"] = name;
            dataRow["author"] = author;
            dataRow["filename"] = filename;
            dataRow["version"] = version;
            dataRow["description"] = description;
            pluginsDB.Tables["pluginsList"].Rows.Add(dataRow);
            return true;
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
