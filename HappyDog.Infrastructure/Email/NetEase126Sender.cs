using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HappyDog.Infrastructure.Email
{
    public class NetEase126Sender : IEmailSender
    {
        public NetEase126Sender(IConfiguration configuration)
        {
            FromAddress = configuration["Email:NetEase126:Account"];
            _password = configuration["Email:NetEase126:Password"];
        }

        public string FromAddress { get; }
        readonly string _password;

        public async Task SendAsync(MailMessage message)
        {
            using (var smpt = new SmtpClient("smtp.126.com"))
            {
                smpt.EnableSsl = true;
                smpt.Credentials = new NetworkCredential(FromAddress, _password);
                await smpt.SendMailAsync(message);
            }
        }
    }
}
