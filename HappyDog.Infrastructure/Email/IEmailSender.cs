using System.Net.Mail;
using System.Threading.Tasks;

namespace HappyDog.Infrastructure.Email
{
    public interface IEmailSender
    {
        string FromAddress { get; }
        Task SendAsync(MailMessage message);
    }
}
