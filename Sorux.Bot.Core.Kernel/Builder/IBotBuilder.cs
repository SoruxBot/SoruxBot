using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sorux.Bot.Core.Kernel.Builder
{
    public interface IBotBuilder
    {
        //用于BotBuilder的低级配置项生成
        IBotBuilder ConfigureRuntimeConfiguration(Action<IConfigurationBuilder> configureDelegate);

        //用于Bot配置项的生成
        IBotBuilder ConfigureBotConfiguration(Action<BotBuilderContext, IConfigurationBuilder> configureDelegate);

        //用于Bot服务的注册生成
        IBotBuilder ConfigureServices(Action<IConfiguration, IServiceCollection> configureDelegate);

        //用于Bot实例的生成
        IBot Build();
    }
}