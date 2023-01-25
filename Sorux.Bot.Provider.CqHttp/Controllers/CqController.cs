using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sorux.Bot.Core.Interface.PluginsSDK.Models;
using Grpc.Net.Client;
using Sorux.Bot.WebgRpc;

namespace Sorux.Bot.Provider.CqHttp.Controllers;

public class CqController : ControllerBase
{
    private ILogger<CqController> _logger;

    GrpcChannel? channel = null;
    Message.MessageClient client = null;

    public CqController(ILogger<CqController> logger, IConfiguration configuration)
    {
        this._logger = logger;
        channel = GrpcChannel.ForAddress(configuration["gRPCHost"]);
        client = new Message.MessageClient(channel);
    }

    [HttpPost]
    [Route("")]
    public void Post([FromBody] JsonObject jsonObject)
    {
        Task.Run(() =>
        {
            string postType = jsonObject["post_type"]!.ToString();
            switch (postType)
            {
                case "message": //消息上报
                    string messageType = jsonObject["message_type"]!.ToString();
                    switch (messageType)
                    {
                        case "private":
                            PrivateMessageHandler(jsonObject); //SoloMessage;qq;message-private[subType]
                            break;
                        case "group":
                            GroupMessageHandler(jsonObject); //GroupMessage;qq;message-group[subType]
                            break;
                        default:
                            break;
                    }

                    break;
                case "notice":
                    string noticeType = jsonObject["notice_type"]!.ToString();
                    switch (noticeType)
                    {
                        case "friend_recall":
                            PrivateMessageRecallHandler(jsonObject); //SoloMessage;qq;notice-friend_call
                            break;
                        case "group_recall":
                            GroupMessageRecallHandler(jsonObject); //NoticeAction;qq;notice-group_call
                            break;
                        case "group_increase":
                            GroupMemberIncreaseHandler(jsonObject); //NoticeAction;qq;notice-group_increase[subType]
                            break;
                        case "group_decrease":
                            GroupMemberDecreaseHandler(jsonObject); //NoticeAction;qq;notice-group_decrease[subType]
                            break;
                        case "group_admin":
                            GroupManagerChangeHandler(jsonObject); //NoticeAction;qq;notice-group_admin[subType]
                            break;
                        case "group_upload":
                            FileUpload(jsonObject); //NoticeAction;qq;notice-group_upload
                            break;
                        case "group_ban":
                            GroupBanMessage(jsonObject); //NoticeAction;qq;notice-group_ban[subType]
                            break;
                        case "friend_add":
                            FriendAdd(jsonObject); //NoticeAction;qq;notice-friend_add
                            break;
                        case "notify":
                            Notify(jsonObject); //NoticeAction;qq;notice-notify
                            break;
                        case "group_card":
                            GroupCardChangHandler(jsonObject); //NoticeAction;qq;notice-group_card
                            break;
                        case "offline_file":
                            OfflineFileHandler(jsonObject); //NoticeAction;qq;notice-offline_file
                            break;
                        case "client_status":
                            ClientChangeHandler(jsonObject); //NoticeAction;qq;notice-client_status
                            break;
                        case "essence":
                            EssenceChangeHandler(jsonObject); //NoticeAction;qq;notice-essence[subType]
                            break;
                        default:
                            break;
                    }

                    break;
                case "request":
                    string requestType = jsonObject["request_type"]!.ToString();
                    switch (requestType)
                    {
                        case "friend":
                            AddFrinedHandler(jsonObject); //NoticeAction;qq;request-friend
                            break;
                        case "group":
                            AddGroupHandler(jsonObject); //NoticeAction;qq;request-group[subType]
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
        });
    }

    public void AddGroupHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["request_type"] = jsonObject["request_type"]!.ToString(),
            ["post_type"   ] = jsonObject["post_type"   ]!.ToString(),
            ["sub_type"    ] = jsonObject["sub_type"    ] == null ? "" : jsonObject["sub_type"]!.ToString(),
            ["comment"     ] = jsonObject["comment"     ] == null ? "" : jsonObject["comment"]!.ToString(),
            ["flag"        ] = jsonObject["flag"        ] == null ? "" : jsonObject["flag"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;request-group" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"] == null ? "" : jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"] == null ? "" : jsonObject["group_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    public void AddFrinedHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["request_type"] = jsonObject["request_type"]!.ToString(),
            ["post_type"   ] = jsonObject["post_type"   ]!.ToString(),
            ["comment"     ] = jsonObject["comment"     ] == null ? "" : jsonObject["comment"]!.ToString(),
            ["flag"        ] = jsonObject["flag"        ] == null ? "" : jsonObject["flag"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;request-friend",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"] == null ? "" : jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = "",
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }


    public void EssenceChangeHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["message_id" ] = jsonObject["message_id" ] == null ? "" : jsonObject["message_id"]!.ToString(),
            ["sub_type"   ] = jsonObject["sub_type"   ] == null ? "" : jsonObject["sub_type"]!.ToString(),
            ["sender_id"  ] = jsonObject["user_id"    ] == null ? "" : jsonObject["sender_id"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-essence" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["operator_id"] == null ? "" : jsonObject["operator_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"] == null ? "" : jsonObject["group_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    public void ClientChangeHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["client"     ] = jsonObject["client"     ] == null ? "" : jsonObject["client"]!.ToString(),
            ["online"     ] = jsonObject["online"     ] == null ? "" : jsonObject["online"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-client_status",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = "",
            TriggerPlatformId = "",
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }


    public void OfflineFileHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["file"       ] = jsonObject["file"       ] == null ? "" : jsonObject["file"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-offline_file",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"] == null ? "" : jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = "",
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    public void GroupCardChangHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["card_old"   ] = jsonObject["card_old"   ] == null ? "" : jsonObject["card_old"]!.ToString(),
            ["card_new"   ] = jsonObject["card_new"   ] == null ? "" : jsonObject["card_new"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-group_card",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"] == null ? "" : jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"] == null ? "" : jsonObject["group_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    public void Notify(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new();
        dictionary["notice_type"] = jsonObject["notice_type"]!.ToString();
        dictionary["post_type"] = jsonObject["post_type"]!.ToString();
        dictionary["title"] = jsonObject["title"] == null ? "" : jsonObject["title"]!.ToString();
        dictionary["honor_type"] = jsonObject["honor_type"] == null ? "" : jsonObject["honor_type"]!.ToString();
        dictionary["target_id"] = jsonObject["target_id"] == null ? "" : jsonObject["target_id"]!.ToString();
        dictionary["sub_type"] = jsonObject["sub_type"] == null ? "" : jsonObject["sub_type"]!.ToString();
        dictionary["sender_id"] = jsonObject["user_id"] == null ? "" : jsonObject["sender_id"]!.ToString();
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-notify" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"] == null ? "" : jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"] == null ? "" : jsonObject["group_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    public void FriendAdd(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["user_id"    ] = jsonObject["user_id"    ] == null ? "" : jsonObject["user_id"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-friend_add",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = "",
            TriggerPlatformId = "",
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    public void GroupBanMessage(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["sub_type"   ] = jsonObject["sub_type"   ] == null ? "" : jsonObject["sub_type"]!.ToString(),
            ["user_id"    ] = jsonObject["user_id"    ] == null ? "" : jsonObject["user_id" ]!.ToString(),
            ["duration"   ] = jsonObject["duration"   ] == null ? "" : jsonObject["duration"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-group_ban" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId         = jsonObject["operator_id"] == null ? "" : jsonObject["operator_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"   ] == null ? "" : jsonObject["group_id"   ]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    public void FileUpload(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new();
        dictionary["notice_type"] = jsonObject["notice_type"]!.ToString();
        dictionary["post_type"  ] = jsonObject["post_type"  ]!.ToString();
        dictionary["file"       ] = jsonObject["file"       ] == null ? "" : jsonObject["file"   ]!.ToString();
        dictionary["user_id"    ] = jsonObject["user_id"    ] == null ? "" : jsonObject["user_id"]!.ToString();
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-group_upload",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = "",
            TriggerPlatformId = jsonObject["group_id"] == null ? "" : jsonObject["group_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }


    public void GroupManagerChangeHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new();
        dictionary["notice_type"] = jsonObject["notice_type"]!.ToString();
        dictionary["post_type"  ] = jsonObject["post_type"  ]!.ToString();
        dictionary["sub_type"   ] = jsonObject["sub_type"   ] == null ? "" : jsonObject["sub_type"]!.ToString();
        dictionary["user_id"    ] = jsonObject["user_id"    ] == null ? "" : jsonObject["user_id" ]!.ToString();
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-group_admin" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = "",
            TriggerPlatformId = jsonObject["group_id"] == null ? "" : jsonObject["group_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }


    public void GroupMemberDecreaseHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["sub_type"   ] = jsonObject["sub_type"   ] == null ? "" : jsonObject["sub_type"]!.ToString(),
            ["user_id"    ] = jsonObject["user_id"    ] == null ? "" : jsonObject["user_id" ]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-group_decrease" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId         = jsonObject["operator_id"] == null ? "" : jsonObject["operator_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"   ] == null ? "" : jsonObject["group_id"   ]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }


    public void GroupMemberIncreaseHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new();
        dictionary["notice_type"] = jsonObject["notice_type"]!.ToString();
        dictionary["post_type"  ] = jsonObject["post_type"  ]!.ToString();
        dictionary["sub_type"   ] = jsonObject["sub_type"   ] == null ? "" : jsonObject["sub_type"]!.ToString();
        dictionary["user_id"    ] = jsonObject["user_id"    ] == null ? "" : jsonObject["user_id" ]!.ToString();
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-group_increase" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId         = jsonObject["operator_id"] == null ? "" : jsonObject["operator_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"   ] == null ? "" : jsonObject["group_id"   ]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }


    private void GroupMessageRecallHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["notice_type"] = jsonObject["notice_type"]!.ToString(),
            ["post_type"  ] = jsonObject["post_type"  ]!.ToString(),
            ["message_id" ] = jsonObject["message_id" ] == null ? "" : jsonObject["message_id"]!.ToString(),
            ["user_id"    ] = jsonObject["user_id"    ]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-group_call",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId         = jsonObject["operator_id"] == null ? "" : jsonObject["operator_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"   ] == null ? "" : jsonObject["group_id"   ]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }


    private void GroupMessageHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["post_type"   ] = jsonObject["post_type"   ]!.ToString(),
            ["message_type"] = jsonObject["message_type"]!.ToString(),
            ["sub_type"    ] = jsonObject["sub_type"    ]!.ToString(),
            ["message_id"  ] = jsonObject["message_id"  ]!.ToString(),
            ["message"     ] = jsonObject["message"     ]!.ToString(),
            ["font"        ] = jsonObject["font"        ]!.ToString(),
            ["sender"      ] = jsonObject["sender"      ]!.ToString(),
            ["anonymous"   ] = jsonObject["anonymous"   ] == null ? "" : jsonObject["anonymous"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "GroupMessage;qq;message-group" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity()
            {
                Meta = jsonObject["raw_message"]!.ToString(),
            },
            MessageEventType = EventType.GroupMessage,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = jsonObject["group_id"] == null ? "" : jsonObject["group_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    private void PrivateMessageRecallHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new();
        dictionary["notice_type"] = jsonObject["notice_type"]!.ToString();
        dictionary["post_type"] = jsonObject["post_type"]!.ToString();
        dictionary["message_id"] = jsonObject["message_id"]!.ToString();
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "NoticeAction;qq;notice-friend_call",
            BotAccount = jsonObject["self_id"]!.ToString(),
            LongMessageContext = null,
            Message = new MessageEntity(),
            MessageEventType = EventType.NoticeAction,
            TargetPlatform = "qq",
            TiedAttribute = "",
            TiedId = "",
            TriggerId = jsonObject["user_id"]!.ToString(),
            TriggerPlatformId = jsonObject["user_id"]!.ToString(),
            UnderProperty = dictionary
        };
        client.MessagePushStack(new MessageRequest() { Payload = JsonConvert.SerializeObject(messageContext) });
    }

    private void PrivateMessageHandler(JsonObject jsonObject)
    {
        Dictionary<string, string> dictionary = new()
        {
            ["post_type"   ] = jsonObject["post_type"   ]!.ToString(),
            ["sub_type"    ] = jsonObject["post_type"   ]!.ToString(),
            ["message_type"] = jsonObject["message_type"]!.ToString(),
            ["message_id"  ] = jsonObject["message_id"  ]!.ToString(),
            ["message"     ] = jsonObject["message"     ]!.ToString(),
            ["font"        ] = jsonObject["font"        ]!.ToString(),
            ["sender"      ] = jsonObject["sender"      ]!.ToString(),
            ["temp_source" ] = jsonObject["temp_source" ] == null ? "" : jsonObject["temp_source"]!.ToString()
        };
        MessageContext messageContext = new MessageContext()
        {
            MessageTime = jsonObject["time"]!.ToString(),
            ActionRoute = "SoloMessage;qq;message-private" + jsonObject["sub_type"]!,
            BotAccount = jsonObject["self_id"]!.ToString(),
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