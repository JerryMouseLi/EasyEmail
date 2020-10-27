using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEmail
{ 
    /// <summary>
    /// 邮件队列缓存
    /// </summary>
    public interface IMailQueueProvider
    {
        /// <summary>
        /// 向队列添加邮件
        /// </summary>
        /// <param name="mailBox"></param>
        void Enqueue(MailBox mailBox);
        /// <summary>
        /// 从队列取出邮件
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        bool TryDequeue(out MailBox mailBox);
        /// <summary>
        /// 当前待发送邮件数量，只读属性
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 当前邮件数量是否为空，只读属性
        /// </summary>
        bool IsEmpty { get; }
    }
}
