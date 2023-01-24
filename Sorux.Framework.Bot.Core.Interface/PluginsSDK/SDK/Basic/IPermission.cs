using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;

public interface IPermission
{
    /// <summary>
    /// 给某人增加权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="permissionType">权限字节点来源类型</param>
    /// <param name="permissionChar">权限字节</param>
    /// <param name="node">节点名</param>
    public bool AddGenericPermission(MessageContext context, PermissionType permissionType, string permissionChar,
        string node);

    /// <summary>
    /// 删除权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="permissionType"></param>
    /// <param name="permissionChar"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool RemoveGenericPermission(MessageContext context, PermissionType permissionType, string permissionChar,
        string node);

    /// <summary>
    /// 查询某物体具有的权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string GetGenericPermission(MessageContext context, string obj);

    /// <summary>
    /// 得到TriggerId所具有的权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="triggerId"></param>
    /// <returns></returns>
    public string GetTriggerIdPermission(MessageContext context, string triggerId);

    /// <summary>
    /// 得到TriggerPlatform所具有的权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="triggerPlatform"></param>
    /// <returns></returns>
    public string GetTriggerPlatformPermission(MessageContext context, string triggerPlatform);

    /// <summary>
    /// 增加某人权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool AddTriggerIdPermission(MessageContext context, string node);

    /// <summary>
    /// 删除某人权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool RemoveTriggerIdPermission(MessageContext context, string node);

    /// <summary>
    /// 增加某群权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool AddTriggerPlatformPermission(MessageContext context, string node);

    /// <summary>
    /// 删除某群权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool RemoveTriggerPlatformPermission(MessageContext context, string node);

    /// <summary>
    /// 判断是否有某权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="permissionType"></param>
    /// <param name="permissionChar"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool GenericHasPermission(MessageContext context, PermissionType permissionType, string permissionChar,
        string node);

    /// <summary>
    /// 某人是否具有某权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool TriggerIdHasPermission(MessageContext context, string node);

    /// <summary>
    /// 某群是否具有某权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool TriggerPlatformHasPermission(MessageContext context, string node);
}