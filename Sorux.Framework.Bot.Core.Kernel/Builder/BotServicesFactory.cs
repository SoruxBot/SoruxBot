using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public class BotServicesFactory
    {
        //单例模型
        private static readonly BotServicesFactory instance = new BotServicesFactory();
        private BotServicesFactory() { }
        public static BotServicesFactory Instance { get { return instance; } }

        //IOC 工厂
        private IServiceCollection? _services;
        public IServiceCollection BuildContainer(IServiceCollection services)
        {
            if (_services != null)
                throw new Exception("重复显式调用BotFactory构造Service Collection方法");
            this._services = services;
            return this._services;
        }

        public IServiceProvider GetProvider()
            => _services.BuildServiceProvider();
    }
}
