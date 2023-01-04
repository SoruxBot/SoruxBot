using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorux.Framework.Bot.Core.Kernel.Interface;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public class Bot : IBot
    {
        public BotContext Context { get; init; }
        public IConfiguration Configuration { get; init; }
        public Bot(BotContext context,IConfiguration configuration) 
        {
            this.Context = context;
            this.Configuration = configuration;
            // _host = Host.CreateDefaultBuilder()
            //     .ConfigureServices(services =>
            //     {
            //         services.AddSingleton(this);
            //         services.AddSingleton<ILoggerService, LoggerService>();
            //         //services.AddHostedService<Work>(); //后台服务请求
            //     })
            //     .Build();
        }
        
        public void AddMsgRequest(string msg)
        {
            this.Context.ServiceProvider.GetRequiredService<IMessageQueue>().SetNextMsg(msg);
        }
        //private IHost _host;
        // public void Dispose()
        // {
        //     //this.Stop();
        // }
        //
        // public void Start()
        // {
        //    //_host.Run();
        // }
        //
        // public void Stop()
        // {
        //     //_host.StopAsync();
        // }

    }
}
