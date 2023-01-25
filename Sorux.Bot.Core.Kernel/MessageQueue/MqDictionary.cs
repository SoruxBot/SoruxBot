using Sorux.Bot.Core.Kernel.Interface;
using Newtonsoft.Json;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Kernel.DataStorage;
using Sorux.Bot.Core.Kernel.Builder;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.Core.Kernel.MessageQueue
{
    public class MqDictionary : IMessageQueue
    {
        private readonly BotContext _botContext;
        private readonly ILoggerService _loggerService;
        private Queue<MessageContext> _Queue = new Queue<MessageContext>(); //MqDictionary是单例生成，此处不需要额外static

        public MqDictionary(BotContext botContext, ILoggerService loggerService)
        {
            this._botContext = botContext;
            this._loggerService = loggerService;
            _loggerService.Info("MqDictionary", "MqDictionary has been initialized.");
            _loggerService.Info("MqDictionary", "MqDictionary's Author: SoruxBot Local Implement. Version:1.0.0");
        }

        public void SetNextMsg(MessageContext value)
        {
            _Queue.Enqueue(value);
        }

        public MessageContext? GetNextMessageRequest() =>
            _Queue.TryDequeue(out MessageContext? value) ? value : null;

        public void RestoreFromLocalStorage()
        {
            _loggerService.Info("MqDictionary", "Restore from the local storage.");
            if (new FileInfo(DsLocalStorage.GetMessageQueuePath()).Exists)
            {
                this._Queue = JsonConvert.DeserializeObject<Queue<MessageContext>>(
                    File.ReadAllText(DsLocalStorage.GetMessageQueuePath()))!;
            }
        }

        public void DisposeFromLocalStorage()
        {
            _loggerService.Info("MqDictionary", "Dispose the local storage.");
            File.Delete(DsLocalStorage.GetMessageQueuePath());
        }

        public void SaveIntoLocalStorage()
        {
            this.DisposeFromLocalStorage();
            File.WriteAllText(DsLocalStorage.GetMessageQueuePath(),
                JsonConvert.SerializeObject(_Queue));
        }
    }
}