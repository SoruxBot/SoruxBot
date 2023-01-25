﻿using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Bot.Core.Kernel.Builder;
using Sorux.Bot.Core.Kernel.Interface;
using Sorux.Bot.Core.Kernel.Plugins;
using Sorux.Bot.Core.Kernel.Utils;

namespace Sorux.Bot.Core.Kernel.APIServices;

public class BasicApi : IBasicAPI
{
    private BotContext _botContext;
    private ILoggerService _loggerService;
    private PluginsHost _pluginsHost;
    private IResponseQueue _responseQueue;

    public BasicApi(BotContext botContext, ILoggerService loggerService, PluginsHost pluginsHost,
        IResponseQueue responseQueue)
    {
        this._botContext = botContext;
        this._loggerService = loggerService;
        this._pluginsHost = pluginsHost;
        this._responseQueue = responseQueue;
    }

    public void Action(ResponseContext response) => _responseQueue.SetNextReponse(response);

    public Task<string> ActionAsync(ResponseContext response) => _pluginsHost.ActionAysnc(response);

    public string ActionCompute(ResponseContext response) => _pluginsHost.ActionCompute(response);

    public void SendPrivateMessage(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerId,
            MessageContent = content,
            ResopnseRoute = "sendPrivateMessage"
        };

        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendPrivateMessage"
        };
        _responseQueue.SetNextReponse(response);
    }

    public void SendGroupMessage(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerPlatformId,
            MessageContent = content,
            ResopnseRoute = "sendGroupMessage"
        };
        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendGroupMessage"
        };
        _responseQueue.SetNextReponse(response);
    }

    public Task<string> SendGroupMessageAsync(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerPlatformId,
            MessageContent = content,
            ResopnseRoute = "sendGroupMessage"
        };
        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendGroupMessage"
        };
        return _pluginsHost.ActionAysnc(response);
    }

    public string SendGroupMessageCompute(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerPlatformId,
            MessageContent = content,
            ResopnseRoute = "sendGroupMessage"
        };
        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendGroupMessage"
        };
        return _pluginsHost.ActionCompute(response);
    }


    public Task<string> SendPrivateMessageAsync(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerId,
            MessageContent = content,
            ResopnseRoute = "sendPrivateMessage"
        };

        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendPrivateMessage"
        };
        return _pluginsHost.ActionAysnc(response);
    }

    public string SendPrivateMessageCompute(MessageContext context, string content)
    {
        ResponseModel responseModel = new()
        {
            Receiver = context.TriggerId,
            MessageContent = content,
            ResopnseRoute = "sendPrivateMessage"
        };

        ResponseContext response = new()
        {
            Message = context,
            ResponseData = responseModel,
            ResponseRoute = "common;sendPrivateMessage"
        };
        return _pluginsHost.ActionCompute(response);
    }
}