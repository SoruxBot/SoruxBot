using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Builder
{
    public interface IBot : IDisposable
    {
        //IOC Container
        BotServicesFactory Services { get; init; }
        IConfiguration Configuration { get; init; }
        /// <summary>
        /// 负责机器人实例的启动，本实例会一直运行直到被组件唤起关闭事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        void Start();
        /// <summary>
        /// 负责机器人实例的关闭
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        void Stop();
    }
}
