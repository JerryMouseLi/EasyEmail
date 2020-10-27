using EasyEmail;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {

            var i = 0;

            //创建服务容器对像
            var serviceCollection = new ServiceCollection();
            //添加服务注册
            ConfigureServices(serviceCollection);
            #region 微软原生依赖注入容器
            //构建ServiceProvider对象
            var serviceProvider = serviceCollection.BuildServiceProvider();
            //获取服务
            var emailManager =serviceProvider.GetService<IMailQueueManager>();
            var emailQueueProvider = serviceProvider.GetService<IMailQueueProvider>();
            var emailQueueService = serviceProvider.GetService<IMailQueueService>();


            emailManager.Run();
            #endregion


            MailBox QqMailbox = new MailBox();


            MailBox WangyiMailbox = new MailBox();

            WangyiMailbox.To = "13737732703@163.com";
            WangyiMailbox.Body = "163fadf邮件类库测试";
            WangyiMailbox.Subject = "16fadfa3测试";


            QqMailbox.To = "87888397@qq.com";
            QqMailbox.Body = "qqfadsfa邮箱测试";
            QqMailbox.Cc = "935467953@qq.com";
            QqMailbox.Subject = "qq邮fadfa箱测试";



            var  _houseKeepTimer = new Timer(  state =>
          {
            if (i % 2 == 0)
            { emailQueueService.Enqueue(QqMailbox); }
            else
            { emailQueueService.Enqueue(WangyiMailbox); }

            Console.WriteLine("加入第"+i+"封邮件。" );
               

            i++;
          }, null, 1, 4000);


            Thread.Sleep(3000);
            emailQueueService.Enqueue(QqMailbox);
            Console.WriteLine("3秒之后完成发一封qq邮箱。");
            Thread.Sleep(3000);

            emailQueueService.Enqueue(WangyiMailbox);
            Console.WriteLine("10秒之后完成发一封163邮箱。");

            Console.ReadLine();
        }
        private static void ConfigureServices(ServiceCollection services)
        {

            services.AddEmailKit(() => 
           {
               EmailConfig emailConfig = new EmailConfig( );
               #region 163网易邮件发送
                emailConfig.EmailSmtpAddress = "smtp.163.com";
                emailConfig.EmalHostPort = 25;
                emailConfig.SendEmailAccount = "13737732703@163.com";
                emailConfig.SendEmailPassWord = "******";
               #endregion

               #region qq 邮件发送
            //  emailConfig.EmailSmtpAddress = "smtp.qq.com";
            //  emailConfig.EmalHostPort = 587;
            //  emailConfig.SendEmailAccount = "87888397@qq.com";
            //  emailConfig.SendEmailPassWord = "*****";
               #endregion

               return emailConfig;
           });
        }
    }
}
