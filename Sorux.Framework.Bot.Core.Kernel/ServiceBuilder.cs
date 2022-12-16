using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Logger;
using Sorux.Framework.Bot.Core.Kernel.MessageQueue;
using Sorux.Framework.Bot.Core.Kernel.Plugins;
using Sorux.Framework.Bot.Core.Kernel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel
{
    public class ServiceBuilder
    {
        private IMessageQueue messageQueue = MqDictionary.GetInstance();
        private ILogger logger = Logger.Logger.GetInstance();
        private IPluginsStorage pluginsStorage = PluginsLocalStorage.GetInstance();
        private IPluginsStoragePermanentAble? pluginsStoragePermanentAble;
        public ServiceBuilder AddMessageQueue(IMessageQueue messageQueue)
        {
            this.messageQueue = messageQueue;
            return this;
        }
        public ServiceBuilder AddLogger(ILogger logger) 
        {
            this.logger = logger;
            return this;
        }
        public ServiceBuilder AddPluginsSupport()
        {
            PluginsService.RegisterPlugins();
            return this;
        }
        public void build()
        {
            Global global = new Global(
                this.logger,
                this.messageQueue,
                this.pluginsStorage);
            if (pluginsStoragePermanentAble != null) 
                global.AddPluginsStoragePerment(pluginsStoragePermanentAble);
        }
    }
}
