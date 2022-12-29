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
        /// 尝试得到优先级，如果返回真则表示对应的优先级可用，如果返回假则说明对应的优先级不可用，同时通过 out int result 返回可以使用的优先级。
        /// 本命令旨在注册指定的优先级，基于优先级唯一的原则，若指定注册的优先级已经被注册了（实际注册顺序为内置 API 遍历目录的顺序），那么返回可用的最靠近的优先级。
        /// 永远满足 privilege <= result
        /// </summary>
        /// <param name="privilege"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetPrivilegeUpper(int privilege, out int result);
        /// <summary>
        /// 如果一个插件没有指明优先级，比如 Privilege 配置为 $Default 那么就依次添加优先级即可。使用本命令可以直接得到可用中最低的优先级。
        /// 如果插件显式指明了优先级，那么使用 TryGetPrivilege
        /// </summary>
        /// <returns></returns>
        int GetLastUsablePrivilege();
        /// <summary>
        /// 移除一个插件，表示的是将插件在内存中移除，而不是物理上移除
        /// </summary>
        /// <param name="name"></param>
        void RemovePlugin(string name);
        /// <summary>
        /// 移除所有的插件，表示的是将所有插件在内存中移除，而不是物理上的移除
        /// </summary>
        void RemoveAllPlugins();
        /// <summary>
        /// 得到对应的委托方法列表，按照优先级排序。
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        //TODO EventType 进行修改，匹配逻辑修改为 头信息匹配+表达式目录树的逻辑匹配。
        // 头信息匹配：决定进入哪一个方法，例如 EventType 的通用匹配或者是母命令的精确匹配
        // 通用匹配：TriggerMessage 为 %*% ，触发所有的规则，然后根据 EventType Filter 进行过滤
        // 精确匹配：TriggerMessage 为 指定消息 ， 触发指定的消息，然后根据 EventType Filter 进行过滤
        // 根据 EventType Filter 来建立方法的等待表
        // 逻辑匹配：归结到 Filter 中，做成扩展方法，用于判断消息是否进入指定的方法中
        List<Func<bool,MessageContext,ILoggerService,IPluginsDataStorage>> GetAction(EventType eventType,string TriggerMessage);
        /// <summary>
        /// 得到指定插件的作者名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string? GetAuthor(string name);
        /// <summary>
        /// 得到指定插件的文件名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string? GetFileName(string name);
        /// <summary>
        /// 得到指定插件的版本名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string? GetVersion(string name);
        /// <summary>
        /// 得到指定插件的描述信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string? GetDescription(string name);
        /// <summary>
        /// 得到指定插件的优先级
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int? GetPrivilege(string name);
        /// <summary>
        /// 得到指定插件的 UUID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string? GetUUID(string name);

        /// <summary>
        /// 得到指定插件的 UUID , 若插件并未注册 UUID， 那么返回 False.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGetUUID(string name, out string uuid);
        /// <summary>
        /// 修改插件的优先级顺序，且返回修改后真实的优先级（修改后的优先级后于或等于指定优先级）
        /// </summary>
        /// <param name="privilege"></param>
        /// <returns></returns>
        public int EditPrivilege(string name,int privilege);
        /// <summary>
        /// 得到指定优先级的插件名称，没有则返回 Null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string? GetPluginByPrivilege(int privilege);
        /// <summary>
        /// 修改插件的优先级顺序，且返回修改后真实的优先级（修改后的优先级先于或者等于指定优先级）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="privilege"></param>
        /// <returns></returns>
        public int EditPrivilegeByUpper(string name, int privilege);
        /// <summary>
        /// 判断插件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExists(string name);
    }
}
