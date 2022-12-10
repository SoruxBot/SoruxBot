using Sorux.Framework.Bot.Core.Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    /// <summary>
    /// Storage of plugins[Chat plugins]
    /// </summary>
    public interface IPluginsStorage
    {
        //If the plugin's name exists , return false.
        bool AddPlugins(string name, string author, string filename, string version, string description);
        bool AddPlugins(string name,string author,string filename,string version,string description,string uuid);
        void RemovePlugins(string name);
        void RemoveAllPlugins();

        //Every event is in a list.
        List<string> GetEventWaitingList(EventType eventType);
        //Get method that is binding to the event type
        string GetEventAction(EventType eventType);
    }
}
