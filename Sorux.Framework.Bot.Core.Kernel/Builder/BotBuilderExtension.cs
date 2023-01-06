using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Kernel.APIServices;
using Sorux.Framework.Bot.Core.Kernel.DataStorage;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public static class BotBuilderExtension
    {
        public static IBotBuilder CreateDefaultBotConfigure(this IBotBuilder builder, string[]? args)
            => builder.ConfigureRuntimeConfiguration(config => ApplyDefaultRuntimeConfiguration(config, args))
                      //注入Bot配置信息，用于存储基本的框架配置信息
                      .ConfigureBotConfiguration((context, config) => ApplyDefaultBotConfiguration(context, config))
                      //注入框架自身的版本信息
                      .ConfigureBotConfiguration((context, config) => ApplyDefaultBotFrameworkInformation(context,config))
                      //配置框架的基本服务信息
                      .ConfigureServices(ApplyDefaultServices)
                      //注入不可变的信息，分开放是因为这四个服务是绝对不会变的
                      .ConfigureServices((config, services) =>
                      {
                          //Bot自身
                          services.AddSingleton<IBot,Bot>();
                          var loggerFactory = LoggerFactory.Create(builder =>
                          {
                              builder.AddConsole();
                              if (config["LoggerDebug"] != null && config["LoggerDebug"]!.Equals("true"))
                              {
                                  builder.AddDebug();
                              }
                              services.AddSingleton(builder);
                          });
                          services.AddSingleton(loggerFactory);
                          services.AddSingleton(typeof(ILogger<>),typeof(Logger<>));
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
                new KeyValuePair<string, string?>("CurrentPath",cwd)
            });
        }

        private static void ApplyDefaultBotFrameworkInformation(BotBuilderContext context, IConfigurationBuilder config)
        {
            //注入基本信息
            config.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("FrameworkVersion",AppSettings.FrameworkVersion),
                new KeyValuePair<string, string?>("CoreKernelVersion",AppSettings.CoreKernelVersion),
                new KeyValuePair<string, string?>("WebDirector",AppSettings.WebDirector),
                new KeyValuePair<string, string?>("WebApiDirector",AppSettings.WebApiDirector)
            });
        }
        
        private static void ApplyDefaultServices(IConfiguration configuration,IServiceCollection services)
        {
            IConfigurationSection section = configuration.GetSection("RuntimeAdapter");
            //直接绑定
            string? mqPath = section.GetSection("MessageQueue")["Path"];
            string? mqModule = section.GetSection("MessageQueue")["Namespace"];
            string? rqPath = section.GetSection("ResponseQueue")["Path"];
            string? rqModule = section.GetSection("ResponseQueue")["Namespace"];
            string? loggerPath = section.GetSection("Logger")["Path"];
            string? loggerModule = section.GetSection("Logger")["Namespace"];
            string? pluginsDataStoragePath = section.GetSection("PluginsDataStorage")["Path"];
            string? pluginsDataStorageModule = section.GetSection("PluginsDataStorage")["Namespace"];
            string? pluginsDataStoragePermenantPath = section.GetSection("PluginsStoragePermanent")["Path"];
            string? pluginsDataStoragePermenantModule = section.GetSection("PluginsStoragePermanent")["Namespace"];
            //管道通信
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
            //回复通信
            if (rqPath == "$BotFramework")
            {
                switch (rqModule)
                {
                    case "MessageQueue.ResponseQueue":
                        services.AddSingleton<IResponseQueue,MessageQueue.ResponseQueue>();
                        break;
                    case "$None":
                        break;
                    default:
                        throw new DllNotFoundException();       
                }
            }
            else
            {
                if (!rqModule!.Equals("$None"))
                {
                    rqPath = rqPath!.Replace("$LocalRunPath", Directory.GetCurrentDirectory());
                    Assembly assembly = Assembly.Load(rqPath);
                    Type type = assembly.GetType(rqModule!) ?? throw new DllNotFoundException();
                    services.AddSingleton<IResponseQueue>(s => (IResponseQueue)Activator.CreateInstance(type)!);
                }
            }
            
            //日志实现
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
            //插件数据文件储存
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
            //插件永久数据存储实现
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
