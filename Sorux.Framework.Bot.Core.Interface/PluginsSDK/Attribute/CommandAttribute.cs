
namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// Mark for commands
    /// The command need not the beginning mark.If need , mark your need explicitly.
    /// the parameter in the <> will be fill automatically with the suitable command patameter
    /// the parameter in the [] is optional ,which means the parameter waiting for the data have default value
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : System.Attribute
    {
        public CommandAttribute(Prefix prefix = Prefix.Global,params string[] command) { }
        //If the prefix is single , the plugins should give the statement out in the Register process.
        //We do not support you use different command prefix in one plugin.
        public enum Prefix
        {
            None = 0,
            Single = 1,
            Global = 2,
        }
    }
}
