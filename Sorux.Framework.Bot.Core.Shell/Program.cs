using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Shell
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var BotInstance = CreateDefaultBotBuilder(args).Build();
            
            builder.Services.AddSingleton(BotInstance);

            builder.Services.AddSingleton<ILoggerService, LoggerService>();

            builder.Services.AddHostedService<Work>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static IBotBuilder CreateDefaultBotBuilder(string[] args)
        {
            BotBuilder botBuilder = new();
            return botBuilder.CreateDefaultBotConfigure(args)
                             .ConfigureBotConfiguration((context, configure) =>
                             {
                                 configure.AddInMemoryCollection(new[]
                                 {
                                    new KeyValuePair<string, string?>("WebListenerPort","7999")
                                 });
                             });
        }
    }
}