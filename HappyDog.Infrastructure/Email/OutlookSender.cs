using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HappyDog.Infrastructure.Email
{
    public class OutlookSender : IEmailSender
    {
        public OutlookSender(IConfiguration configuration)
        {
            FromAddress = configuration["AppSettings:Email:Outlook:Account"];
            password = configuration["AppSettings:Email:Outlook:Password"];
        }

        public string FromAddress { get; }
        readonly string password;

        public async Task SendAsync(MailMessage message)
        {
            using (var smpt = new SmtpClient("smtp-mail.outlook.com", 587))
            {
                smpt.EnableSsl = true;
                smpt.Credentials = new NetworkCredential(FromAddress, password);
                await smpt.SendMailAsync(message);
            }
        }
    }
}
