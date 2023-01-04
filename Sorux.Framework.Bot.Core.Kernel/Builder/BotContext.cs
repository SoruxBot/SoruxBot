using Microsoft.Extensions.DependencyInjection;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public class BotContext
    {
        //单例模型
        private static readonly BotContext instance = new BotContext();
        private BotContext() { }
        public static BotContext Instance { get { return instance; } }
        
        //IOC 工厂
        private IServiceCollection? _services;

        private IServiceProvider _serviceProvider;
        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }
        public void CreateContainer(IServiceCollection serviceCollection)
        {
            this._services = serviceCollection;
        }

        public BotContext ConfigureService(Action<IServiceCollection> services) 
        {
            services(_services!);
            return this;
        }

        public void BuildContainer()
            => this._serviceProvider = _services!.BuildServiceProvider();
    }
}
