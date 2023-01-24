using RestSharp;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Models;

public class PluginsHostDescriptor
{
    public string Platform { get; set; }

    public string HttpPostJsonPath { get; set; }

    public string NetWorkHttpPostPath { get; set; }

    public RestClient FutureHost { get; set; }

    public RestClient CommonHost { get; set; }
}