using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage
{
    public class PluginsLocalStorage : IPluginsStorage
    {
        private ILoggerService _loggerService;
        private BotContext _botContext;
        public PluginsLocalStorage(BotContext context, ILoggerService loggerService)
        {
            this._botContext = context;
            this._loggerService = loggerService;
        }
        
        public bool AddPlugin(string name, string author, string filename, string version, string description, int privilege)
        {
            throw new NotImplementedException();
        }

        public bool AddPlugin(string name, string author, string filename, string version, string description, string uuid,
            int privilege)
        {
            throw new NotImplementedException();
        }

        public bool TryGetPrivilege(int privilege, out int result)
        {
            throw new NotImplementedException();
        }

        public int GetLastUsablePrivilege()
        {
            throw new NotImplementedException();
        }

        public void RemovePlugin(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllPlugins()
        {
            throw new NotImplementedException();
        }

        public List<Func<bool, MessageContext, ILoggerService, IPluginsDataStorage>> GetAction(EventType eventType)
        {
            throw new NotImplementedException();
        }
    }
}
