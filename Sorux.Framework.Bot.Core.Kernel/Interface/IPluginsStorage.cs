using Sorux.Framework.Bot.Core.Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    /// <summary>
    /// 存储插件信息
    /// </summary>
    public interface IPluginsStorage
    {
        //如果想要注册的插件其对应的名称存在（名称为主键），那么就返回失败且不允许插件注册。
        //由于名称为包名，在名称相同的情况下我们更认为是插件的不同版本的重复注册。
        bool AddPlugin(string name, string author, string filename, string version, string description,int privilege);
        //UUID 指的是框架支持的云端服务器对应的唯一UUID值
        bool AddPlugin(string name,string author,string filename,string version,string description,string uuid,int privilege);
        /// <summary>
        /// 尝试得到优先级，如果返回真则表示对应的优先级可用，如果返回假则说明对应的优先级不可用，同时通过 out int result 返回可以使用的优先级。
        /// 本命令旨在注册指定的优先级，基于优先级唯一的原则，若指定注册的优先级已经被注册了（实际注册顺序为内置 API 遍历目录的顺序），那么返回可用的最靠近的优先级。
        /// 永远满足 privilege >= result
        /// </summary>
        /// <param name="privilege"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetPrivilege(int privilege, out int result);
        /// <summary>
        /// 如果一个插件没有指明优先级，比如 Privilege 配置为 $Default 那么就依次添加优先级即可。使用本命令可以直接得到可用中最低的优先级。
        /// 如果插件显式指明了优先级，那么使用 TryGetPrivilege
        /// </summary>
        /// <returns></returns>
        int GetLastUsablePrivilege();
        //移除一个插件
        void RemovePlugin(string name);
        //移除所有的插件
        void RemoveAllPlugins();
        /// <summary>
        /// 得到对应的委托方法列表
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        List<Func<bool,MessageContext,ILoggerService,IPluginsDataStorage>> GetAction(EventType eventType);
    }
}
