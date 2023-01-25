namespace Sorux.Bot.Core.Kernel.Plugins.Models;

public class PluginsActionParameter
{
    public bool IsOptional { get; set; }

    public Type ParameterType { get; set; }

    public string Name { get; set; }
}