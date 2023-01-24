﻿using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.MessageQueue;

/// <summary>
/// 内置的 ResponseQueue 实现
/// </summary>
public class ResponseQueue : IResponseQueue
{
    private BotContext _botContext;
    private ILoggerService _loggerService;
    private Queue<ResponseContext> _Queue = new Queue<ResponseContext>();

    public ResponseQueue(BotContext botContext, ILoggerService loggerService)
    {
        this._botContext = botContext;
        this._loggerService = loggerService;
        _loggerService.Info("ResponseQueue", "ReponseQueue has been initialized.");
        _loggerService.Info("ResponseQueue", "ReponseQueue's Author: SoruxBot Local Implement. Version:1.0.0");
    }


    public void SetNextReponse(ResponseContext context)
    {
        _Queue.Enqueue(context);
    }

    public ResponseContext? GetNextResponse()
        => _Queue.TryDequeue(out ResponseContext? value) == true ? value : null;

    public void RestoreFromLocalStorage()
    {
        _loggerService.Info("ResponseQueue", "Restore from the local storage.");
        if (new FileInfo(DsLocalStorage.GetResponseQueuePath()).Exists)
        {
            this._Queue = JsonConvert.DeserializeObject<Queue<ResponseContext>>(
                File.ReadAllText(DsLocalStorage.GetResponseQueuePath()))!;
        }
    }

    public void SaveIntoLocalStorage()
    {
        this.DisposeFromLocalStorage();
        File.WriteAllText(DsLocalStorage.GetResponseQueuePath(),
            JsonConvert.SerializeObject(_Queue));
    }

    public void DisposeFromLocalStorage()
    {
        _loggerService.Info("ResponseQueue", "Dispose the local storage.");
        new FileInfo(DsLocalStorage.GetResponseQueuePath()).Delete();
    }
}