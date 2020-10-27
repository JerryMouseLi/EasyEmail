using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEmail
{
    /// <summary>
    /// 邮件队列服务
    /// </summary>
    public interface IMailQueueService
    {
        /// <summary>
        /// 向邮件发送队列添加邮件
        /// </summary>
        /// <param name="box"></param>
        void Enqueue(MailBox box);
    }
}
