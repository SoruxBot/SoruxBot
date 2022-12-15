using Sorux.Framework.Bot.Core.Kernel.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Microsoft.Extensions.Logging;
using Sorux.Framework.Bot.Core.Kernel.Service;

namespace Sorux.Framework.Bot.Core.Kernel.MessageQueue
{
    public class MqDictionary : IMessageQueue
    {
        private static readonly MqDictionary mqDictionary = new MqDictionary();
        private MqDictionary() { }
        public static MqDictionary GetInstance() => mqDictionary;

        private Queue<string> _Queue = new Queue<string>();
        public void DisposeFromLocalStorage()
        {
            Global.GetGlobal().Logger.LogInformation("[SoruxBot][MQ]Dispose all local Storage");
            new FileInfo(DsLocalStorage.GetMessageQueuePath()).Delete();
        }

        public string GetNextMessageRequest() => _Queue.Dequeue();
        public void RestoreFromLocalStorage()
        {
            Global.GetGlobal().Logger.LogInformation("[SoruxBot][MQ]Restore from local storage");
            if (new FileInfo(DsLocalStorage.GetMessageQueuePath()).Exists)
            {
                this._Queue = JsonConvert.DeserializeObject<Queue<string>>(
                                File.ReadAllText(DsLocalStorage.GetMessageQueuePath()));
            }
        }

        public void SaveIntoLocalStorage()
        {
            this.DisposeFromLocalStorage();
            File.WriteAllText(DsLocalStorage.GetMessageQueuePath(),
                JsonConvert.SerializeObject(_Queue));
        }

        public void SetNextMsg(string value)
        {
            Global.GetGlobal().Logger.LogInformation("[SoruxBot][MQ]Message enqueue.",value);
            _Queue.Enqueue(value);
        }

    }
}
