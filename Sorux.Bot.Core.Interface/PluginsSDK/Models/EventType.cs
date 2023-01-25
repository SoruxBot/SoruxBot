
namespace Sorux.Bot.Core.Interface.PluginsSDK.Models
{
    /// <summary>
    /// 事件类型，表示消息的来源状态。
    /// </summary>
    public enum EventType
    {
        //单独对话，即两个人对话的场景，例如QQ的企业临时咨询，私聊对话，群临时对话，微信的验证对话等等
        SoloMessage = 0,
        //群组对话，即多人对话的场景，例如QQ的多人对话，Telegram的通知频道和群聊频道等等.
        GroupMessage = 1,
        //频道对话，即在同一主体下不同区域的对话，如QQ的A频道的B房间，如Discord的类似的形式
        ChannelMessage = 2,
        //通知事件，即不来源于个体的消息发送方，其也可以有对应的GroupId，ChannelId等，但是发送账号为-1，即一个无意义的账号
        NoticeAction = 3,
        //处理事件，同通知事件，但是处理事件可以被拦截，而通知事件不需要被拦截。处理事件需要对传递的对象做一个及时（或者保留上下文刻意地延迟）的回应.
        NoticeFunction = 4
    }
}
