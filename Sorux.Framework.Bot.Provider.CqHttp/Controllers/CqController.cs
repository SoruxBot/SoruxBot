using System.Runtime.Intrinsics.X86;
using System.Text.Json.Nodes;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Grpc.Net.Client;
using Sorux.Framework.Bot.WebgRpc;

namespace Sorux.Framework.Bot.Provider.CqHttp.Controllers;

public class CqController : ControllerBase
{
    private ILogger<CqController> _logger;

    GrpcChannel? channel = null;
    Message.MessageClient client = null;

    public CqController(ILogger<CqController> logger,IConfiguration configuration)
    {
        this._logger = logger;
        channel = GrpcChannel.ForAddress(configuration["gRPCHost"]);
        client = new Message.MessageClient(channel);
    }
    
    [HttpPost]
    [Route("")]
    public void Post([FromBody] JsonObject jsonObject)
    {
        string post_Type = jsonObject["post_type"]!.ToString();
        switch (post_Type)
        {
            case "message":
                PrivateMessageHandler(jsonObject);
                break;
            default:
                break;
        }
    }

    private void PrivateMessageHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new();
        dictionary["post_type"] = jsonObject["post_type"]!.ToString();
        dictionary["message_type"] = jsonObject["message_type"]!.ToString();
        dictionary["message_id"] = jsonObject["message_id"]!.ToString();
        dictionary["message"] = jsonObject["message"]!.ToString();
        dictionary["font"] = jsonObject["font"]!.ToString();
        dictionary["temp_source"] = jsonObject["temp_source"] == null ? "" : jsonObject["temp_source"]!.ToString();
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "SoloMessage" + ";qq;" + jsonObject["message_type"]! + "-" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString()!,
            LongMessageContext = null,
            Message = new MessageEntity()
            {
                Meta = jsonObject["raw_message"]!.ToString(),
            },
            MessageEventType = EventType.SoloMessage,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = jsonObject["user_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }
}