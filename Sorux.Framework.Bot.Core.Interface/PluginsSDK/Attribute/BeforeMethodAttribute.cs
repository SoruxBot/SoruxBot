using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
/// <summary>
/// 表示在执行消息方法前执行相应的命令
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class BeforeMethodAttribute : System.Attribute
{
    public BeforeMethodAttribute()
    {
        
    }
}