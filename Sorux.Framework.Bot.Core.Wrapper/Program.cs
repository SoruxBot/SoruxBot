using Sorux.Framework.Bot.Core.Kernel;
using Sorux.Framework.Bot.Core.Kernel.MessageQueue;
using Sorux.Framework.Bot.Core.Kernel.Logger;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Microsoft.Extensions.Configuration;

namespace Sorux.Framework.Bot.Core.Wrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = CreateDefaultBotBuilder(args).Build();
            app.Start();
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