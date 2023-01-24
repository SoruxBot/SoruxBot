namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public class BotBuilderContext
    {
        public BotBuilderContext(BuildEnvironmentType buildEnvironmentType, RuntimeSystemType runtimeSystemType)
            => (BuildEnvironment, RuntimeSystem) = (buildEnvironmentType, runtimeSystemType);

        public BuildEnvironmentType BuildEnvironment { get; init; }

        public RuntimeSystemType RuntimeSystem { get; init; }
    }

    public enum BuildEnvironmentType
    {
        Normal,
        Debug,
        Developer
    }

    public enum RuntimeSystemType
    {
        Windows,
        Linux,
        MacOS,
        Android,
        Unknown
    }
}