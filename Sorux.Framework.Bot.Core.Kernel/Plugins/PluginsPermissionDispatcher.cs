using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins;

public class PluginsPermissionDispatcher
{

    private ILoggerService _loggerService;
    private PermissionStorage _permissionStorage;
    
    public PluginsPermissionDispatcher(ILoggerService loggerService,PermissionStorage permissionStorage)
    {
        this._loggerService = loggerService;
        this._permissionStorage = permissionStorage;
    }
    
    public bool IsContinue(MessageContext messageContext,PluginsActionDescriptor pluginsActionDescriptor)
    {
        if (pluginsActionDescriptor.PluginsPermissionDescriptor == null)
            return true;
        PluginsPermissionDescriptor pluginsPermissionDescriptor = pluginsActionDescriptor.PluginsPermissionDescriptor;
        string conditionNode = pluginsPermissionDescriptor.PermissionNode + "SoruxBot" + messageContext.TargetPlatform + "SoruxBot" +
                               pluginsPermissionDescriptor.FilterAction(messageContext);
        return _permissionStorage.GetNodeCondition(conditionNode);
    }
}