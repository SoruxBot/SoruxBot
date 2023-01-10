using Sorux.Framework.Bot.Core.Kernel.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Kernel.Plugins;
using Sorux.Framework.Bot.WebgRpc.Services;
using Grpc.Core;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
using Sorux.Framework.Bot.Core.Kernel.Utils;
using Sorux.Framework.Bot.WebgRpc;

namespace Sorux.Framework.Bot.Core.Wrapper
{
    internal class Program
    {
        private static int _gloabalPluginsLimitTime;
        private static ILoggerService _loggerService;
        static void Main(string[] args)
        {
            //机器人创建
            var app = CreateDefaultBotBuilder(args).Build();
            //插件注册
            app.Context.ServiceProvider.GetRequiredService<PluginsService>().RegisterPlugins();
            //协议层 Wrapper 注册
            app.Context.ServiceProvider.GetRequiredService<PluginsHost>().Register();
            //gRPC 端口合法性审核，审核通过则返回 Port
            int port = TryGetPort(app);
            //构建 gRPC服务
            Server server = BuildGrpcServer(app);
            //运行 gRPC服务
            server.Start();
            //获取超时时间配置
            _gloabalPluginsLimitTime
                = int.Parse(app.Configuration.GetRequiredSection("PluginsDispatcher")["DefaultPluginResponseLimit"]!);
            //日志服务
            _loggerService = app.Context.ServiceProvider.GetRequiredService<ILoggerService>();
            //机器人启动 启动在主线程看为阻塞式，BotStart 方法不会返回
            BotStart(app);
        }

        private static void BotStart(IBot app)
        {
            //入栈
            IMessageQueue messageQueue = app.Context.ServiceProvider.GetRequiredService<IMessageQueue>();
            PluginsDispatcher pluginsDispatcher = app.Context.ServiceProvider.GetRequiredService<PluginsDispatcher>();
            PluginsCommandLexer pluginsCommandLexer =
                app.Context.ServiceProvider.GetRequiredService<PluginsCommandLexer>();
            //出栈
            IResponseQueue responseQueue = app.Context.ServiceProvider.GetRequiredService<IResponseQueue>();
            PluginsHost pluginsHost = app.Context.ServiceProvider.GetRequiredService<PluginsHost>();
            Task.Run(() =>
            {
                while (true)
                {
                    //消息队列
                    MessageContext? messageContext = messageQueue.GetNextMessageRequest();
                    if (messageContext != null)
                    {
                        Task.Run( () =>
                        {
                            string route = null;
                            if (messageContext.Message != null)//消息，需要经过命名路由
                            {
                                route = messageContext.ActionRoute + "/" +
                                        messageContext.Message.GetRawMessage().Split(" ")[0];
                            }
                            else//非消息，不需要经过命名路由
                            {
                                route = messageContext.ActionRoute + "/";
                            }
                    
                            List<PluginsActionDescriptor>? list = pluginsDispatcher.GetAction(route,ref messageContext);
                            if (list != null)
                                list.ForEach(sp =>
                                {
                                    if (messageContext.Message != null && 
                                        messageContext.Message.MsgState != PluginFucFlag.MsgIgnored)
                                        messageContext.Message.MsgState = pluginsCommandLexer.PluginAction(messageContext, sp);
                                });
                        });
                    }
                    Thread.Sleep(0);
                }
            });
            while (true)
            {
                //请求队列
                ResponseContext? responseContext = responseQueue.GetNextResponse();
                if(responseContext != null)
                {
                    //调度返回值，注意，这里调用的是没有返回值的
                    Task.Run(() =>
                    {
                        pluginsHost.Dispatch(responseContext);
                    });
                }
                Thread.Sleep(0);
            }
        }
        
        private static IBotBuilder CreateDefaultBotBuilder(string[] args)
        {
            BotBuilder botBuilder = new();
            return botBuilder.ConfigureBotConfiguration((context, configure) =>
                             {
                                 configure.AddInMemoryCollection(new[]
                                 {
                                     //本两个设置默认不起作用，如果想要启用请根据 GetUrl 和 TryGetPort 方法内的注释修改代码以启用本设置
                                     new KeyValuePair<string, string?>("WebListenerUrl","localhost"),
                                     new KeyValuePair<string, string?>("WebListenerPort","7151"),
                                     //本设置默认关闭且不可以通过配置项打开，如有需要可以打开本行
                                     //本设置的 Debug 针对于框架内部，一般情况下不需要开启本项，即使是生产环境的调试，如果是开发框架，建议打开
                                     new KeyValuePair<string, string?>("LoggerDebug","false")
                                 });
                             }).CreateDefaultBotConfigure(args)
                             .ConfigureServices((config, services) =>
                             {
                                 //此处用于注册机器人的启动流程，顺序为：机器人组件组装->机器人内置服务分配->机器人实例组装->机器人额外启动流程（此处）
                                 //注册机器人插件服务，若注释本两行则机器人不会加载插件
                                 PluginsService.ConfigurePluginsServices(services);
                             });
        }

        private static Server BuildGrpcServer(IBot app) 
            => new Server {
                Services = { Message.BindService(
                new MessageTransmission(app,app.Context.ServiceProvider.GetRequiredService<ILoggerService>())) },
                Ports = { new ServerPort(GetUrl(app), TryGetPort(app), ServerCredentials.Insecure) } };
        
        private static int TryGetPort(IBot app)
        {
            //取消上面两行的注释，并注释返回前的两行可以转而使用框架启动流程内注册的 WebListenerPort 以方便调试
            //对外发布或者 Pr 的时候请注意调整成为 Release状态 ，即从配置文件获取地址
            
            //if (!int.TryParse(app.Configuration["WebListenerPort"], out int port))
            //   throw new Exception("Error Port");
            if (!int.TryParse(app.Configuration.GetRequiredSection("WebLister")["WebListenerPort"], out int port))
                throw new Exception("Error Port");
            return port;
        }

        private static string GetUrl(IBot app)
        {
            //取消上面一行的注释，并注释返回前的一行可以转而使用框架启动流程内注册的 WebListenerUrl 以方便调试
            //对外发布或者 Pr 的时候请注意调整成为 Release状态 ，即从配置文件获取地址
            
            //return app.Configuration["WebListenerUrl"]!;
            return app.Configuration.GetRequiredSection("WebLister")["WebListenerUrl"]!;
        }
        
    }
}