using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.APIServices;

public class PermissionService : IPermission
{
    private PermissionStorage _permissionStorage;
    public PermissionService(PermissionStorage permissionStorage)
    {
        this._permissionStorage = permissionStorage;
    }
    
    public bool AddGenericPermission(MessageContext context, PermissionType permissionType, string permissionChar, string node)
    {
        string addingNode = node + "SoruxBot" + context.TargetPlatform + "SoruxBot";
        string condition;
        if (permissionType == PermissionType.BasicPermission)
        {
            switch (permissionChar)
            {
                case "TriggerId":
                    condition = "TriggerId" + context.TriggerId;
                    addingNode = addingNode + condition;
                    break;
                case "TriggerPlatformId":
                    condition = "TriggerPlatformId" + context.TriggerPlatformId;
                    addingNode = addingNode + condition;
                    break;
                default:
                    return false;
            }
        }
        else
        {
            condition = permissionChar + context.UnderProperty[permissionChar];
            addingNode = addingNode + condition;
        }

        _permissionStorage
            .AddPermission(context.TargetPlatform +  "SoruxBot" + condition,
                node, addingNode);
        return true;
    }

    public bool RemoveGenericPermission(MessageContext context, PermissionType permissionType, string permissionChar, string node)
    {
        string addingNode = node + "SoruxBot" + context.TargetPlatform + "SoruxBot";
        string condition;
        if (permissionType == PermissionType.BasicPermission)
        {
            switch (permissionChar)
            {
                case "TriggerId":
                    condition = "TriggerId" + context.TriggerId;
                    addingNode = addingNode + condition;
                    break;
                case "TriggerPlatformId":
                    condition = "TriggerPlatformId" + context.TriggerPlatformId;
                    addingNode = addingNode + condition;
                    break;
                default:
                    return false;
            }
        }
        else
        {
            condition = permissionChar + context.UnderProperty[permissionChar];
            addingNode = addingNode + condition;
        }
        _permissionStorage
            .RemovePermission(context.TargetPlatform +  "SoruxBot" + condition,
                node, addingNode);
        return true;
    }

    public string GetGenericPermission(MessageContext context, string obj)
    {
        return _permissionStorage.GetPersonPermissionList(context.TargetPlatform + "SoruxBot" + obj);
    }

    public string GetTriggerIdPermission(MessageContext context, string triggerId)
    {
        return GetGenericPermission(context, "TriggerId" + triggerId);
    }

    public string GetTriggerPlatformPermission(MessageContext context, string triggerPlatform)
    {
        return GetGenericPermission(context, "TriggerPlatform" + triggerPlatform);
    }

    public bool AddTriggerIdPermission(MessageContext context, string node)
    {
        return AddGenericPermission(context, PermissionType.BasicPermission, "TriggerId", node);
    }

    public bool RemoveTriggerIdPermission(MessageContext context, string node)
    {
        return RemoveGenericPermission(context, PermissionType.BasicPermission, "TriggerId", node);
    }

    public bool AddTriggerPlatformPermission(MessageContext context, string node)
    {
        return AddGenericPermission(context, PermissionType.BasicPermission, "TriggerPlatform", node);
    }

    public bool RemoveTriggerPlatformPermission(MessageContext context, string node)
    {
        return RemoveGenericPermission(context, PermissionType.BasicPermission, "TriggerPlatform", node);
    }

    public bool GenericHasPermission(MessageContext context, PermissionType permissionType, string permissionChar, string node)
    {
        string addingNode = node + "SoruxBot" + context.TargetPlatform + "SoruxBot";
        string condition;
        if (permissionType == PermissionType.BasicPermission)
        {
            switch (permissionChar)
            {
                case "TriggerId":
                    condition = "TriggerId" + context.TriggerId;
                    addingNode = addingNode + condition;
                    break;
                case "TriggerPlatformId":
                    condition = "TriggerPlatformId" + context.TriggerPlatformId;
                    addingNode = addingNode + condition;
                    break;
                default:
                    return false;
            }
        }
        else
        {
            condition = permissionChar + context.UnderProperty[permissionChar];
            addingNode = addingNode + condition;
        }

        return _permissionStorage.GetNodeCondition(addingNode);
    }

    public bool TriggerIdHasPermission(MessageContext context, string node)
    {
        return GenericHasPermission(context, PermissionType.BasicPermission, "TriggerId", node);
    }

    public bool TriggerPlatformHasPermission(MessageContext context, string node)
    {
        return GenericHasPermission(context, PermissionType.BasicPermission, "TriggerPlatform", node);
    }
}