using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEmail
{
    public static class EmailUtilitiExtension
    {
        public static IServiceCollection AddEmailKit(this IServiceCollection services, Func<EmailConfig> emailFactory)
        {

            services.AddSingleton<IMailQueueManager,MailQueueManager>(provider => 
            {
                var x = provider.GetRequiredService<IMailQueueProvider>();
                
                return new MailQueueManager(x, emailFactory);
            });
            services.AddSingleton<IMailQueueProvider, MailQueueProvider>();
            services.AddSingleton<IMailQueueService, MailQueueService>();
            return services;
        }
    }
}
