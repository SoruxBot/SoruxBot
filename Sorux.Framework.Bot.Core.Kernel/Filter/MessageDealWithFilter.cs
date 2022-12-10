using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Filter
{
    public class MessageDealWithFilter : System.Attribute
    {
        MessageDealWithFilter(DealWithType dealWithType = DealWithType.None) { }

        public enum DealWithType
        {
            None = 1,
            OnlyManager = 2
        }
    }
}
