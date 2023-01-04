using Microsoft.Extensions.Configuration;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public interface IBot //: IDisposable
    {
        //IOC Container
        BotContext Context { get; init; }
        IConfiguration Configuration { get; init; }
        // /// <summary>
        // /// 负责机器人实例的启动，本实例会一直运行直到被组件唤起关闭事件
        // /// </summary>
        // /// <param name="cancellationToken"></param>
        // /// <returns></returns>
        // void Start();
        // /// <summary>
        // /// 负责机器人实例的关闭
        // /// </summary>
        // /// <param name="cancellationToken"></param>
        // /// <returns></returns>
        // void Stop();
        /// <summary>
        /// 添加消息进入消息队列
        /// </summary>
        /// <returns></returns>
        void AddMsgRequest(string msg);
    }
}
