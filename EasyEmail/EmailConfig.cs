using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace EasyEmail
{
    /// <summary>
    /// 邮件可配置项
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// 邮箱SMTP服务器地址
        /// </summary>
        public string EmailSmtpAddress { get; set; }
        /// <summary>
        /// 邮箱服务器端口
        /// </summary>
        public int  EmalHostPort{ get; set; }
        /// <summary>
        /// 发送邮箱地址
        /// </summary>
        public string SendEmailAccount { get; set; }
        /// <summary>
        /// 发送邮箱密码
        /// </summary>
        public string SendEmailPassWord { get; set; }
    }
}
