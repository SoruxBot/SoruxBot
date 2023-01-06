using Sorux.Framework.Bot.Core.Kernel.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Kernel.Plugins;
using Sorux.Framework.Bot.WebgRpc.Services;
using Grpc.Core;
using Sorux.Framework.Bot.Core.Kernel.Utils;
using Sorux.Framework.Bot.WebgRpc;

namespace Sorux.Framework.Bot.Core.Wrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = CreateDefaultBotBuilder(args).Build();
            //插件注册
            app.Context.ServiceProvider.GetRequiredService<PluginsService>().RegisterPlugins();

            if (!int.TryParse(app.Configuration["WebListenerPort"], out int port))
                throw new Exception("Error Port");

            Server server = new Server
            {

                Services = { Message.BindService(
                    new MessageTransmission(app,app.Context.ServiceProvider.GetRequiredService<ILoggerService>())) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }

            };
            server.Start();
            
            while (true)
            {
                
            }
        }

        private static IBotBuilder CreateDefaultBotBuilder(string[] args)
        {
            BotBuilder botBuilder = new();
            return botBuilder.ConfigureBotConfiguration((context, configure) =>
                             {
                                 configure.AddInMemoryCollection(new[]
                                 {
                                     new KeyValuePair<string, string?>("WebListenerPort","7151"),
                                     new KeyValuePair<string, string?>("LoggerDebug","true")
                                 });
                             }).CreateDefaultBotConfigure(args)
                             .ConfigureServices((config, services) =>
                             {
                                 //此处用于注册机器人的启动流程，顺序为：机器人组件组装->机器人内置服务分配->机器人实例组装->机器人额外启动流程（此处）
                                 //注册机器人插件服务，若注释本两行则机器人不会加载插件
                                 PluginsService.ConfigurePluginsServices(services);
                             });
        }
    }
}