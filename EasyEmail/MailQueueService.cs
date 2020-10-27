using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEmail
{
    /// <summary>
    /// Offer the Mail Send Queue Service
    /// </summary>
    public class MailQueueService : IMailQueueService
    {
        /// <summary>
        /// Offer the Mail Queue Cache
        /// </summary>
        private readonly IMailQueueProvider _provider;

        /// <summary>
        /// Init the Queue Cache
        /// </summary>
        /// <param name="provider"></param>
        public MailQueueService(IMailQueueProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Add the Email to the Queue Service
        /// </summary>
        /// <param name="box"></param>
        public void Enqueue(MailBox box)
        {
            _provider.Enqueue(box);
        }
    }
}
