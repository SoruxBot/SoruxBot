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
        public IServiceCollection BuildContainer(IServiceCollection services)
        {
            if (_services != null)
                throw new Exception("重复显式调用BotFactory构造Service Collection方法");
            this._services = services;
            return this._services;
        }

        public BotContext ConfigureService(Action<IServiceCollection> services) 
        {
            services(_services!);
            return this;
        }

        public IServiceProvider GetProvider()
            => _services!.BuildServiceProvider();
    }
}
