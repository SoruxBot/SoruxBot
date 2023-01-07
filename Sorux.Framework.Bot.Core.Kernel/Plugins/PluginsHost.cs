using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins;

public class PluginsHost
{
    private IBot _bot;
    private ILoggerService _loggerService;
    public PluginsHost(IBot bot, ILoggerService loggerService)
    {
        this._bot = bot;
        this._loggerService = loggerService;
    }
    /// <summary>
    /// 表示需要被匹配的 Url ，其中 _host 为 [route,url]
    /// </summary>
    private Dictionary<string, PluginsHostDescriptor> _host = new ();
    /// <summary>
    /// 注册回调特性
    /// </summary>
    public void Register()
    {
        IConfigurationSection section = _bot.Configuration.GetRequiredSection("ProviderConfig");
        bool loop = true;
        int count = 0;
        while (loop)
        {
            //判断选项是否超过一个：
            var test = section.GetSection("ProviderItem:0");
            if (test["Platform"] == null || test["Platform"] == "")
            {
                //选项不超过一个
                var row = section.GetSection("ProviderItem");
                PluginsHostDescriptor pluginsHostDescriptor = row.Get<PluginsHostDescriptor>()!;
                pluginsHostDescriptor.CommonHost = new RestClient(pluginsHostDescriptor.HttpPostJsonPath);
                pluginsHostDescriptor.FutureHost = new RestClient(pluginsHostDescriptor.NetWorkHttpPostPath);
                _host.Add(pluginsHostDescriptor.Platform,pluginsHostDescriptor);
                loop = false;
            }
            else
            {
                var row = section.GetSection("ProviderItem:" + count);
                if (row["Platform"] != null && row["Platform"] != "")
                {
                    PluginsHostDescriptor pluginsHostDescriptor = row.Get<PluginsHostDescriptor>()!;
                    pluginsHostDescriptor.CommonHost = new RestClient(pluginsHostDescriptor.HttpPostJsonPath);
                    pluginsHostDescriptor.FutureHost = new RestClient(pluginsHostDescriptor.NetWorkHttpPostPath);
                    _host.Add(pluginsHostDescriptor.Platform,pluginsHostDescriptor);
                    count++;
                }
                else
                {
                    loop = false;
                }   
            }
        }
    }
    /// <summary>
    /// 分配调度 Response
    /// 由本方法执行的 API，表示插件主动舍弃对协议层 API 请求后的返回值
    /// </summary>
    /// <param name="response"></param>
    public void Dispatch(ResponseContext response)
    {
        //response 格式为  Platform;Url
        if (response.ResponseRoute.Split(";")[0].Equals("common")) //common表示平台抽象的协议
        {
            var request = new RestRequest("APIPost",Method.Post);
            request.AddJsonBody(response.ResponseData);
            _host[response.Message.TargetPlatform].CommonHost.Execute(request);
        }
        else
        {
            var request = new RestRequest(response.ResponseRoute.Split(";")[1],Method.Post);
            request.AddJsonBody(response.ResponseData);
            _host[response.Message.TargetPlatform].FutureHost.Execute(request);
        }
        
    }
    
    /// <summary>
    /// 直接执行 Action
    /// 由本方法执行的 API 请求会返回 JSON 格式的返回值
    /// 本方法以异步执行
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public async Task<string> ActionAysnc(ResponseContext response)
    {
        if (response.ResponseRoute.Split(";")[0].Equals("common")) //common表示平台抽象的协议
        {
            var request = new RestRequest("APIPost",Method.Post);
            request.AddJsonBody(response.ResponseData);
            var result = await _host[response.Message.TargetPlatform].CommonHost.ExecuteAsync(request);
            return result.Content!;
        }
        else
        {
            var request = new RestRequest(response.ResponseRoute.Split(";")[1],Method.Post);
            request.AddJsonBody(response.ResponseData);
            var result = await _host[response.Message.TargetPlatform].FutureHost.ExecuteAsync(request);
            return result.Content!;
        }
    }
    /// <summary>
    /// 直接执行 Action
    /// 由本方法执行的 API 请求会返回 JSON 格式的返回值
    /// 本方法以同步执行
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public string ActionCompute(ResponseContext response)
    {
        if (response.ResponseRoute.Split(";")[0].Equals("common")) //common表示平台抽象的协议
        {
            var request = new RestRequest("APIPost",Method.Post);
            request.AddJsonBody(response.ResponseData);
            var result = _host[response.Message.TargetPlatform].CommonHost.Execute(request);
            return result.Content!;
        }
        else
        {
            var request = new RestRequest(response.ResponseRoute.Split(";")[1],Method.Post);
            request.AddJsonBody(response.ResponseData);
            var result = _host[response.Message.TargetPlatform].FutureHost.Execute(request);
            return result.Content!;
        }
    }
}