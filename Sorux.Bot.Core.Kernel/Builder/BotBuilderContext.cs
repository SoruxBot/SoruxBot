namespace Sorux.Bot.Core.Kernel.Builder
{
    public class BotBuilderContext
    {
        public BotBuilderContext(BuildEnvironmentType buildEnvironmentType)
            => (BuildEnvironment) = (buildEnvironmentType);

        public BuildEnvironmentType BuildEnvironment { get; init; }
    }

    public enum BuildEnvironmentType
    {
        Normal,
        Debug,
        Developer
    }
}