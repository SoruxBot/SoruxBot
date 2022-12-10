using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    public interface ILogger
    {
        void LogInformation(string log);
        void LogWarn(string log);
        void LogError(string log);
        void LogFatal(string log);
        void LogInformation(string log, params object[] paras);
        void LogWarn(string log, params object[] paras);
        void LogError(string log, params object[] paras);
        void LogFatal(string log, params object[] paras);
    }
}
