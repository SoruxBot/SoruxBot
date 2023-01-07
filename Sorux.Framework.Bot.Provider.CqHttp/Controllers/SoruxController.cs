using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Sorux.Framework.Bot.Provider.CqHttp.Controllers;

public class SoruxController : ControllerBase
{
    private ILogger<CqController> _logger;
    private RestClient _host;

    public SoruxController(ILogger<CqController> logger,IConfiguration configuration)
    {
        this._logger = logger;
        _host = new RestClient(configuration["GoCqHost"]);
        logger.LogInformation("Listening:" + configuration["GoCqHost"]);
    }

    [HttpPost]
    [Route("APIPost")]
    public string Post([FromBody] JsonObject jsonObject)
    {
        ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(jsonObject.ToJsonString())!;
        switch (responseModel.ResopnseRoute)
        {
            case "sendPrivateMessage":
                return SendPrivateMessage(responseModel);
            default:
                return "";
        }
    }


    private string SendPrivateMessage(ResponseModel responseModel)
    {
        var request = new RestRequest("send_private_msg",Method.Post);
        request.AddJsonBody(new
        {
            user_id = responseModel.Receiver,
            message = responseModel.MessageContent
        });
        var result = _host.Execute(request);
        return result.Content!;
    }
}