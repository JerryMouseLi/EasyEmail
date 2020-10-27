using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace EasyEmail
{
    /// <summary>
    /// 邮件队列缓存
    /// </summary>
    public class MailQueueProvider : IMailQueueProvider
    {
        /// <summary>
        /// 邮件队列缓存
        /// </summary>
        public static BlockingCollection<MailBox> _mailQueue { get; } = new BlockingCollection<MailBox>();
        /// <summary>
        /// 队列待发送邮件数量
        /// </summary>
        public int Count =>_mailQueue.Count;
        /// <summary>
        /// 队列是否为空
        /// </summary>
        public bool IsEmpty => _mailQueue.Count==0?true:false;
        /// <summary>
        /// 向队列添加邮件
        /// </summary>
        /// <param name="mailBox"></param>
        public void Enqueue(MailBox mailBox)
        {
            _mailQueue.TryAdd(mailBox);
        }
        /// <summary>
        /// 从队列取出邮件
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        public bool TryDequeue(out MailBox mailBox)
        {
            return _mailQueue.TryTake(out mailBox, -1);
        }
    }
}
