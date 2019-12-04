using HappyDog.Domain.DataTransferObjects.Comment;
using MailKit.Net.Smtp;
using Markdig;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.Text;
using System.Threading.Tasks;

namespace HappyDog.Domain.Postman
{
    public class CommentNotificationPostman : ICommentNotificationPostman
    {
        public CommentNotificationPostman(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        readonly IConfiguration _configuration;

        public async Task PostAsync(PostCommentDto dto)
        {
            var builder = new StringBuilder();
            var pipeline = new MarkdownPipelineBuilder()
                .UsePipeTables()
                .Build();
            builder.Append(Markdown.ToHtml(dto.Content, pipeline));
            string link = _configuration["Comment:EmailNotification:LinkPrefix"] + dto.ArticleId;
            builder.AppendLine($"<br /> <a href=\"{link}\">{link}</a>");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["Email:NetEase126:Account"]));
            message.To.Add(new MailboxAddress(_configuration["Comment:EmailNotification:ToAddress"]));
            message.Subject = "doghappy.wang 有新评论了";
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = builder.ToString()
            };
            using var client = new SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            await client.ConnectAsync(_configuration["Email:NetEase126:Host"]);
            await client.AuthenticateAsync(_configuration["Email:NetEase126:Account"], _configuration["Email:NetEase126:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
