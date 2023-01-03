using System.Reflection;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins;
/// <summary>
/// 插件调度器，负责分配事件到具体的插件。
/// </summary>
public class PluginsDispatcher
{
    private BotContext _botContext;
    private ILoggerService _loggerService;
    private IPluginsStorage _pluginsStorage;

    public PluginsDispatcher(BotContext botContext,ILoggerService loggerService,IPluginsStorage pluginsStorage)
    {
        this._botContext = botContext;
        this._loggerService = loggerService;
        this._pluginsStorage = pluginsStorage;
    }
    
    //插件按照触发条件可以分为选项式命令触发和事件触发
    //前者针对某个特定 EventType 的某个特定的语句触发某个特定的方法
    //后者针对某个通用的 EventType 进行触发
    private Dictionary<string, string> _matchList = new Dictionary<string, string>();
    /// <summary>
    /// 注册指令路由
    /// </summary>
    /// <param name="filepath"></param>
    /// <param name="name"></param>
    public void RegisterCommandRoute(string filepath,string name)
    {
        Assembly assembly = Assembly.LoadFile(filepath);


        Type[] types = assembly.GetExportedTypes();
        foreach (var type in types)
        {
            
        }
    }
}