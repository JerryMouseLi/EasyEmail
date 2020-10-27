using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEmail
{
    /// <summary>
    /// 邮件的封装类
    /// </summary>
    public class MailBox
    {
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 邮件抄送地址列表
        /// </summary>
        public string Cc { get; set; }
     //   public IEnumerable<string> Cc { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件发送地址
        /// </summary>
        public string To { get; set; }
    }
}
