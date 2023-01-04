using Sorux.Framework.Bot.Core.Kernel.Interface;
using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.MessageQueue
{
    public class MqDictionary : IMessageQueue
    {
        private readonly BotContext _botContext;
        private readonly ILoggerService _loggerService;
        private Queue<string> _Queue = new Queue<string>(); //MqDictionary是单例生成，此处不需要额外static
        public MqDictionary(BotContext botContext,ILoggerService loggerService)
        {
            this._botContext = botContext;
            this._loggerService = loggerService;
            _loggerService.Info("MqDictionary","MqDictionary has been initialized.");
            _loggerService.Info("MqDictionary","MqDictionary's Author: SoruxBot Local Implement. Version:1.0.0");
        }
        
        public void SetNextMsg(string value)
        {
            _loggerService.Info("MqDictionary","Message enqueue.",value);
            _Queue.Enqueue(value);
        }
        public string? GetNextMessageRequest() => _Queue.TryDequeue(out string? value) == true ? value : null;
        public void RestoreFromLocalStorage()
        {
            _loggerService.Info("MqDictionary","Restore from the local storage.");
            if (new FileInfo(DsLocalStorage.GetMessageQueuePath()).Exists)
            {
                this._Queue = JsonConvert.DeserializeObject<Queue<string>>(
                                File.ReadAllText(DsLocalStorage.GetMessageQueuePath()));
            }
        }

        public void DisposeFromLocalStorage()
        {
            _loggerService.Info("MqDictionary","Dispose the local storage.");
            new FileInfo(DsLocalStorage.GetMessageQueuePath()).Delete();
        }
        
        public void SaveIntoLocalStorage()
        {
            this.DisposeFromLocalStorage();
            File.WriteAllText(DsLocalStorage.GetMessageQueuePath(),
                JsonConvert.SerializeObject(_Queue));
        }

    }
}
