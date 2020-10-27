using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEmail
{
    /// <summary>
    /// 邮件队列管理器
    /// </summary>
    public interface IMailQueueManager
    {
        /// <summary>
        /// 运行邮件队列管理器
        /// </summary>
        void Run();
        /// <summary>
        /// 停止邮件队列管理器
        /// </summary>
        void Stop();
        /// <summary>
        /// 邮件队列管理器运行标志
        /// </summary>
        bool IsRunning { get; }
        /// <summary>
        /// 队列中待发送邮件的数量
        /// </summary>
        int Count { get; }
    }
}
