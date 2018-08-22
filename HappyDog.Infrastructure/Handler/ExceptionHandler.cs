using HappyDog.Infrastructure.Email;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HappyDog.Infrastructure.Handler
{
    public static class ExceptionHandler
    {
        public static async Task SendEmailAsync(Exception ex, IEmailSender sender)
        {
            await Task.Factory.StartNew(async () =>
            {
                var message = new MailMessage(sender.FromAddress, sender.FromAddress)
                {
                    Subject = "[异常]api.doghappy.wang",
                    Body = ex.ToString()
                };
                await sender.SendAsync(message);
            });
        }
    }
}
