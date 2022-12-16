using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public static class BotBuilderExtension
    {
        public static IBotBuilder CreateDefaultBotConfigure(this BotBuilder builder, string[]? args)
            => builder.ConfigureRuntimeConfiguration(config => ApplyDefaultRuntimeConfiguration(config, args))
                      .ConfigureBotConfiguration((context, config) => ApplyDefaultBotConfiguration(context, config))
                      .ConfigureServices(ApplyDefaultServices)
                      .ConfigureServices((config, services) =>
                      {
                          services.AddSingleton<IBot,Bot>();
                          services.AddSingleton<ILoggerService, LoggerService>();
                      });

        private static void ApplyDefaultRuntimeConfiguration(IConfigurationBuilder config, string[]? args)
        {
            //添加CommandLine 的 args
            if (args is { Length: >0})
            {
                config.AddCommandLine(args);
            }
        }

        private static void ApplyDefaultBotConfiguration(BotBuilderContext context,IConfigurationBuilder config)
        {
            //Bot Configuration主要负责配置连接器的组装等操作
            string cwd = Environment.CurrentDirectory;
            config.AddXmlFile("BotConfiguration.xml",optional:false,reloadOnChange:false);//配置为单例模式
            //注入BotContext
            //注入CurrentPath【Controller不一定来源于当前目录】
            config.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("ContextBuildEnvironment",context.BuildEnvironment.ToString()),
                new KeyValuePair<string, string?>("ContextRuntimeSystem",context.RuntimeSystem.ToString()),
                new KeyValuePair<string,string?>("CurrentPath",cwd)
            });
        }
        private static void ApplyDefaultServices(IConfiguration configuration,IServiceCollection services)
        {
            IConfigurationSection section = configuration.GetSection("RuntimeAdapter");
            string? mqPath = section.GetSection("MessageQueue")["Path"];
            string? mqModule = section.GetSection("MessageQueue")["Namespace"];
            string? loggerPath = section.GetSection("Logger")["Path"];
            string? loggerModule = section.GetSection("Logger")["Namespace"];
            string? pluginsDataStoragePath = section.GetSection("PluginsDataStorage")["Path"];
            string? pluginsDataStorageModule = section.GetSection("PluginsDataStorage")["Namespace"];
            string? pluginsDataStoragePermenantPath = section.GetSection("PluginsStoragePermanent")["Path"];
            string? pluginsDataStoragePermenantModule = section.GetSection("PluginsStoragePermanent")["Namespace"];

            if (mqPath == "$BotFramework")
            {
                switch (mqModule)
                {
                    case "MessageQueue.MqDictionary":
                        services.AddSingleton<IMessageQueue, MessageQueue.MqDictionary>();
                        break;
                    case "$None":
                        break;
                    default:
                        throw new DllNotFoundException();       
                }
            }
            else
            {
                if (!mqModule!.Equals("$None"))
                {
                    mqPath = mqPath!.Replace("$LocalRunPath", Directory.GetCurrentDirectory());
                    Assembly assembly = Assembly.Load(mqPath);
                    Type type = assembly.GetType(mqModule!) ?? throw new DllNotFoundException();
                    services.AddSingleton<IMessageQueue>(s => (IMessageQueue)Activator.CreateInstance(type)!);
                }
            }

            if (loggerPath == "$BotFramework")
            {
                switch (loggerModule)
                {
                    case "$None":
                        break;
                    default:
                        throw new DllNotFoundException();
                }
            }
            else
            {
                if (!loggerModule!.Equals("$None"))
                {
                    loggerPath = loggerPath!.Replace("$LocalRunPath", Directory.GetCurrentDirectory());
                    Assembly assembly = Assembly.Load(loggerPath);
                    Type type = assembly.GetType(loggerModule!) ?? throw new DllNotFoundException();
                    services.AddSingleton<ILogger>(s => (ILogger)Activator.CreateInstance(type)!);
                }
            }

            if (pluginsDataStoragePath == "$BotFramework")
            {
                switch (pluginsDataStorageModule)
                {
                    case "$None":
                        break;
                    default:
                        throw new DllNotFoundException();
                }
            }
            else
            {
                if (!pluginsDataStorageModule!.Equals("$None"))
                {
                    pluginsDataStoragePath = pluginsDataStoragePath!.Replace("$LocalRunPath", Directory.GetCurrentDirectory());
                    Assembly assembly = Assembly.Load(pluginsDataStoragePath);
                    Type type = assembly.GetType(pluginsDataStorageModule!) ?? throw new DllNotFoundException();
                    services.AddSingleton<IPluginsDataStorage>(s => (IPluginsDataStorage)Activator.CreateInstance(type)!);
                }
            }

            if (pluginsDataStoragePermenantPath == "$BotFramework")
            {
                switch (pluginsDataStorageModule)
                {
                    case "$None":
                        break;
                    default:
                        throw new DllNotFoundException();
                }
            }
            else
            {
                if (!pluginsDataStorageModule!.Equals("$None"))
                {
                    pluginsDataStoragePermenantPath = pluginsDataStoragePermenantPath!.Replace("$LocalRunPath", Directory.GetCurrentDirectory());
                    Assembly assembly = Assembly.Load(pluginsDataStoragePermenantPath);
                    Type type = assembly.GetType(pluginsDataStoragePermenantModule!) ?? throw new DllNotFoundException();
                    services.AddSingleton<IPluginsStoragePermanentAble>(s => (IPluginsStoragePermanentAble)Activator.CreateInstance(type)!);
                }
            }

        }
    }
}
