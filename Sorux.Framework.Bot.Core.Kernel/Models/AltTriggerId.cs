namespace Sorux.Framework.Bot.Core.Kernel.Models;
/// <summary>
/// 用于替代非真实个体，类似于掩码号
/// </summary>
public enum AltTriggerId
{
    //通知账号，表示事件的发送者
    NotifyAccount = -1,
    //匿名账号Id，若发送者账号为-2则表示是匿名账号。
    //注：此处可能需要对QQ平台的匿名账号做特殊处理：80000000为低版本QQ默认的匿名账号
    Anonymous = -2,
    //Bot自身的消息发送
    BotSelfAccount = -3,    
    //来源于程序发送的消息
    ProgramMessage = -4,
    //来源于程序发送的通知消息
    ProgramNotice = -5,
    //其他账号，注意：本账号可以被正常解析，但是不属于任何一个AltTrigger的类别，因此归为此类，如果插件有额外的处理逻辑，可以使用自己标识的特定的OthersAccountId
    //如果插件没有额外的处理逻辑，请统一使用本枚举，即以-99来进行代替
    OthersAccount = -99,
    //无法被正常记载的账号，可能是错误，也可能是信息丢失或者是各种各样的原因
    ErrorAccount = -100,
}