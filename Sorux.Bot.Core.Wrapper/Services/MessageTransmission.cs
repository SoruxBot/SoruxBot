﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Kernel.Builder;
using Sorux.Bot.Core.Kernel.Interface;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.WebgRpc.Services
{
    public class MessageTransmission : Message.MessageBase
    {
        private IBot _bot;
        private ILoggerService _logger;
        private IMessageQueue _messageQueue;

        public MessageTransmission(IBot bot, ILoggerService logger)
        {
            this._bot = bot;
            this._logger = logger;
            this._messageQueue = _bot.Context.ServiceProvider.GetRequiredService<IMessageQueue>();
        }

        public override Task<Empty> MessagePushStack(MessageRequest request, ServerCallContext context)
        {
            _messageQueue.SetNextMsg(JsonConvert.DeserializeObject<MessageContext>(request.Payload)!);
            return Task.FromResult(new Empty());
        }
    }
}