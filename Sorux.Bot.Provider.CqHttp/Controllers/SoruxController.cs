using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Bot.Provider.CqHttp.Controllers;

public class SoruxController : ControllerBase
{
    private ILogger<CqController> _logger;
    private RestClient _host;

    public SoruxController(ILogger<CqController> logger, IConfiguration configuration)
    {
        this._logger = logger;
        _host = new RestClient(configuration["GoCqHost"]);
    }

    [HttpPost]
    [Microsoft.AspNetCore.Mvc.Route("APIPost")]
    public string Post([FromBody] JsonObject jsonObject)
    {
        ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(jsonObject.ToJsonString())!;
        return responseModel.ResopnseRoute switch
        {
            "sendPrivateMessage" => SendPrivateMessage(responseModel),
            "sendGroupMessage" => SendGroupMessage(responseModel),
            _ => "Error Request for goHttp, please check your version."
        };
    }

    private string SendGroupMessage(ResponseModel responseModel)
    {
        var request = new RestRequest("send_group_msg", Method.Post);
        request.AddJsonBody(new
        {
            group_id = responseModel.Receiver,
            message = responseModel.MessageContent
        });
        var result = _host.Execute(request);
        return result.Content!;
    }

    private string SendPrivateMessage(ResponseModel responseModel)
    {
        var request = new RestRequest("send_private_msg", Method.Post);
        request.AddJsonBody(new
        {
            user_id = responseModel.Receiver,
            message = responseModel.MessageContent
        });
        var result = _host.Execute(request);
        return result.Content!;
    }
}