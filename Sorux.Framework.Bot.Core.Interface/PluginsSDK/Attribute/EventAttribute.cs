using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// For special event
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EventAttribute : System.Attribute
    {
        public EventAttribute(EventType eventType) { }

    }
}
