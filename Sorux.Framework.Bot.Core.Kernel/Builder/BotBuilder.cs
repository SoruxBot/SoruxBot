using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public class BotBuilder : IBotBuilder
    {
        //被建造的对象列表
        private List<Action<IConfigurationBuilder>> _configureRuntimeActions =new();
        private List<Action<BotBuilderContext,IConfigurationBuilder>> _configureBotActions = new();
        private List<Action<IConfiguration, IServiceCollection>> _configureServicesActions = new();
        //机器人上下文
        private BotBuilderContext? _botBuilderContext;
        //实例生成器，IOC容器暴露给外界的接口
        private IServiceProvider? _serviceProvider;
        //机器人配置，用于配置机器人项
        private IConfiguration? _appConfiguration;
        //Runtime配置，用于收集当前环境信息和配置“低配置项”
        private IConfiguration? _RuntimeConfiguration;
        //机器人的IOC容器
        private IServiceCollection _services;
        //机器人实例的生成工厂
        private BotContext _servicesFactory = BotContext.Instance;
        public BotBuilder()
        {
            _services = new ServiceCollection();
        }

        public IBot Build()
        {
            //初始化Runtime配置
            InitBotRuntimeActions();
            //初始化上下文
            InitContextBuild();
            //初始化Bot配置【单例模式】
            InitBotActions();
            //初始化IOC容器
            InitServices();
            //注册Bot配置的接口
            _servicesFactory.ConfigureService(services =>
            {
                services.AddSingleton(_servicesFactory);
                services.AddSingleton(_appConfiguration!);
            });
            return _servicesFactory.GetProvider().GetRequiredService<IBot>();
        }
        private void InitServices()
        {
            var services = new ServiceCollection();
            foreach (Action<IConfiguration, IServiceCollection> configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(_appConfiguration!, services);
            }
            _services= _servicesFactory.BuildContainer(services);
            _serviceProvider = _servicesFactory.GetProvider();
        }

        private void InitContextBuild() 
        {
            #region OS Information Generate
            RuntimeSystemType runtimeSystemType;
            if (System.OperatingSystem.IsWindows())
            {
                runtimeSystemType = RuntimeSystemType.Windows;
            }
            else if (System.OperatingSystem.IsMacOS())
            {
                runtimeSystemType = RuntimeSystemType.MacOS;
            }
            else if (System.OperatingSystem.IsLinux())
            {
                runtimeSystemType = RuntimeSystemType.Android;
            }
            else if (System.OperatingSystem.IsLinux())
            {
                runtimeSystemType = RuntimeSystemType.Linux;
            }
            else
            {
                runtimeSystemType = RuntimeSystemType.Unknown;
            }
            #endregion
            BuildEnvironmentType bet = _RuntimeConfiguration["section:RuntimeSettings:key:RuntimeType"] switch { 
                "Debug"     => BuildEnvironmentType.Debug,
                "Developer" => BuildEnvironmentType.Developer,
                _           => BuildEnvironmentType.Normal
            };
            _botBuilderContext = new BotBuilderContext(bet, runtimeSystemType);
        }
        private void InitBotRuntimeActions()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            foreach (var buildAction in _configureRuntimeActions)
            {
                buildAction(configBuilder);
            }
            _RuntimeConfiguration= configBuilder.Build();
        }

        private void InitBotActions()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            foreach (var buildAction in _configureBotActions)
            {
                buildAction(_botBuilderContext!,configBuilder);
            }
            _appConfiguration = configBuilder.Build();
        }

        public IBotBuilder ConfigureBotConfiguration(Action<BotBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _configureBotActions.Add(configureDelegate);
            return this;
        }

        public IBotBuilder ConfigureRuntimeConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            _configureRuntimeActions.Add(configureDelegate);
            return this;
        }

        public IBotBuilder ConfigureServices(Action<IConfiguration, IServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate);
            return this;
        }
    }
}
