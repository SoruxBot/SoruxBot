using Sorux.Framework.Bot.Core.Kernel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Service
{
    public class Global
    {
        private static Global global = null;
        public IMessageQueue messageQueue { get; init; }
        public ILogger Logger { get; init; }
        public Global(ILogger logger, IMessageQueue messageQueue)
        {
            this.messageQueue = messageQueue;
            this.Logger = logger;
            if (global != null)
                throw new Exception("Cannot Register two Global instances.");
        }
        public static Global GetGlobal() => global;
    }
}
