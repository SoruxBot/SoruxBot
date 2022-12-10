using Sorux.Framework.Bot.Core.Kernel;
using Sorux.Framework.Bot.Core.Kernel.MessageQueue;
using Sorux.Framework.Bot.Core.Kernel.Logger;
namespace Sorux.Framework.Bot.Core.Wrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceBuilder serviceBuilder = new ServiceBuilder();
            //Service builder
            serviceBuilder.AddMessageQueue(MqDictionary.GetInstance())
                          .AddLogger(Logger.GetInstance())
                          .AddPluginsSupport()
                          .build();

        }
    }
}