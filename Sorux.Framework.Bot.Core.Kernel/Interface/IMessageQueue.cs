using Sorux.Framework.Bot.Core.Kernel.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    public interface IMessageQueue
    {
        //Get request in the queue.
        public string GetNextMessageRequest();
        //Put request in the queue
        public void SetNextMsg(string value);
        //Temp storage in order to prevent abnormal event.
        public void RestoreFromLocalStorage();
        public void SaveIntoLocalStorage();
        public void DisposeFromLocalStorage();
    }
}
