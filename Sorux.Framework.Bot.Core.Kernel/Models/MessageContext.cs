using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Models
{
    public class MessageContext
    {
        /// <summary>
        /// Type: Message,Announce
        /// </summary>
        public string MsgType;
        /// <summary>
        /// Platform: Console,QQ,WeXin,Telegram,Kook,discord,etc.
        /// </summary>
        public string Platform;
        /// <summary>
        /// MessageContent:The main body of one message.
        /// </summary>
        public string Meta;
        /// <summary>
        /// Sender:The unique mark from the targeted platform.
        /// </summary>
        public string Sender;
        /// <summary>
        /// SenderFamily:The family of sender. e.g.the group of QQ.When it comes to independent message,the
        /// SenderFamily is the Sender itself.
        /// </summary>
        public string SenderFamily;
        /// <summary>
        /// SendToBot:The bot that receive the message.
        /// </summary>
        public string SendToBot;
        /// <summary>
        /// EventType:Obey EventType Target at special method controller.
        /// </summary>
        public EventType EventType;
        /// <summary>
        /// ExtraEventType:Exclude from standard event type.
        /// </summary>
        public string ExtraEventType;
    }
}
