using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace EasyEmail
{
    public class MailQueueManager : IMailQueueManager
    {

        private  SmtpClient _smtpClient;
        private readonly IMailQueueProvider _provider;
        Func<EmailConfig> _factory;
       // private readonly EmailConfig _option;

        public EmailConfig _option { get; set; }
        private bool _isRunning = false;
        private bool _tryStop = false;
        private Thread _thread;

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public MailQueueManager(IMailQueueProvider provider, Func<EmailConfig> factory)
        {
             _factory=factory;
             
            _provider = provider;
        }

        /// <summary>
        /// 正在运行
        /// </summary>
        public bool IsRunning => _isRunning;

        /// <summary>
        /// 计数
        /// </summary>
        public int Count => _provider.Count;

        /// <summary>
        /// 启动队列
        /// </summary>
        public void Run()
        {

            if (_isRunning || (_thread != null && _thread.IsAlive))
            {
                //  _logger.LogWarning("已经运行，又被启动了,新线程启动已经取消");
                Console.WriteLine("已经运行，又被启动了,新线程启动已经取消");
                return;
            }
            _isRunning = true;
            _thread = new Thread(StartSendMail)
            {
                Name = "IbmsEmailQueue",
                IsBackground = true,
            };
                //_logger.LogInformation("线程即将启动");
            Console.WriteLine("线程即将启动");
            _thread.Start();
           // _logger.LogInformation("线程已经启动，线程Id是：{0}", _thread.ManagedThreadId);
            Console.WriteLine("线程已经启动，线程Id是：{0}", _thread.ManagedThreadId);
        }

        /// <summary>
        /// 停止队列
        /// </summary>
        public void Stop()
        {
            if (_tryStop)
            {
                return;
            }
            _tryStop = true;
        }

        private void StartSendMail()
        {
            var sw = new Stopwatch();
            try
            {
                while (true)
                {
                    if (_tryStop)
                    {
                        break;
                    }
                    if (_provider.TryDequeue(out MailBox box))
                    {
                        try {

                            //     _logger.LogInformation("开始发送邮件 标题：{0},收件人 {1}", box.Subject, box.To);

                            _option = _factory();
                            Console.WriteLine("开始发送邮件 标题：{0},收件人 {1}", box.Subject, box.To);
                            
                           // sw.Restart();
                            SendMail(box);
                            //  sw.Stop();
                            //   _logger.LogInformation("发送邮件结束标题：{0},收件人 {1},耗时{2}", box.Subject, box.To, sw.Elapsed.TotalSeconds);
                            //  Console.WriteLine("发送邮件结束标题：{0},收件人 {1},耗时{2}", box.Subject, box.To, sw.Elapsed.TotalSeconds);
                            Console.WriteLine("发送邮件结束标题：{0},收件人 {1}", box.Subject, box.To);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("发送邮件异常" + ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "循环中出错,线程即将结束");
                Console.WriteLine( "循环中出错,线程即将结束"+ex);
                _isRunning = false;
            }

          //  _logger.LogInformation("邮件发送线程即将停止，人为跳出循环，没有异常发生");
            Console.WriteLine("邮件发送线程即将停止，人为跳出循环，没有异常发生");
            _tryStop = false;
            _isRunning = false;
        }

        private void SendMail(MailBox box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            try
            {
                MailMessage message = ConvertToMailMessage(box);
                SendMail(message);
            }
            catch (Exception exception)
            {
                //_logger.LogError(exception, "发送邮件发生异常主题：{0},收件人：{1}", box.Subject, box.To);
                Console.WriteLine("发送邮件发生异常主题：{0},收件人：{1}", box.Subject, box.To+"异常："+ exception );
            }
            finally
            {
            }
        }

        private MailMessage ConvertToMailMessage(MailBox box)
        {
            var message = new MailMessage();

            message.From = new MailAddress(_option.SendEmailAccount); 
           
            if (!box.To.Any())
            {
                throw new ArgumentNullException("to必须含有值");
            }
            message.To.Add(box.To);
            if (box.Cc != null && box.Cc.Any())
            {
                message.CC.Add(box.Cc);
            }

            message.Subject = box.Subject;

            message.Body = box.Body;
            return message;
        }

        private void SendMail(MailMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            try
            {
                _smtpClient = new SmtpClient(_option.EmailSmtpAddress);
                _smtpClient.Credentials = new System.Net.NetworkCredential(
                   _option.SendEmailAccount, //邮箱名
                  _option.SendEmailPassWord);//邮箱密码
                _smtpClient.Port =_option.EmalHostPort;
                _smtpClient.SendAsync(message, "userToken");
            }
            finally
            {
            }
        }


    }
}
