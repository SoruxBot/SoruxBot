using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Logger
{
    public class Logger : ILogger
    {
        private static readonly Logger logger = new Logger();
        private Logger() { }
        public static Logger GetInstance() => logger;
        public void LogError(string log)
        {
            //throw new NotImplementedException();
        }

        public void LogError(string log, params object[] paras)
        {
            //throw new NotImplementedException();
        }

        public void LogFatal(string log)
        {
            //throw new NotImplementedException();
        }

        public void LogFatal(string log, params object[] paras)
        {
            //throw new NotImplementedException();
        }

        public void LogInformation(string log)
        {
            //throw new NotImplementedException();
        }

        public void LogInformation(string log, params object[] paras)
        {
            //throw new NotImplementedException();
        }

        public void LogWarn(string log)
        {
            //throw new NotImplementedException();
        }

        public void LogWarn(string log, params object[] paras)
        {
            //throw new NotImplementedException();
        }
    }
}
