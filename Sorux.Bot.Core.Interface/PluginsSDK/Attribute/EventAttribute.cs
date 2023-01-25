using Sorux.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// For special event
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EventAttribute : System.Attribute
    {
        public EventType EventType { get; init; }

        public EventAttribute(EventType eventType)
        {
            EventType = eventType;
        }
    }
}