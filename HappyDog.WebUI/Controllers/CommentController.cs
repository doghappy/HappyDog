using System.Threading.Tasks;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using HappyDog.WebUI.Extensions;
using Edi.Captcha;
using HappyDog.Infrastructure.Email;
using System.Net.Mail;
using System.Text;
using Markdig;

namespace HappyDog.WebUI.Controllers
{
    public class CommentController : Controller
    {
        public CommentController(
            CommentService commentService,
            ISessionBasedCaptcha captcha,
            IEmailSender emailSender)
        {
            _commentService = commentService;
            _captcha = captcha;
            _emailSender = emailSender;
        }

        readonly CommentService _commentService;
        readonly ISessionBasedCaptcha _captcha;
        readonly IEmailSender _emailSender;

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(PostCommentDto dto)
        {
            if (ModelState.IsValid)
            {
                if (_captcha.ValidateCaptchaCode(dto.Code, HttpContext.Session))
                {
                    dto.IPv4 = Request.Host.ToString();
                    await _commentService.CreateAsync(dto);
                    System.Console.WriteLine("===========1");
                    await Task.Factory.StartNew(async () =>
                    {
                        System.Console.WriteLine("===========2");
                        var builder = new StringBuilder();
                        var pipeline = new MarkdownPipelineBuilder()
                            .UsePipeTables()
                            .Build();
                        builder.Append(Markdown.ToHtml(dto.Content, pipeline));
                        string link = $"https://doghappy.wang/Article/Detail/{dto.ArticleId}";
                        builder.AppendLine($"<br /> <a href=\"{link}\">{link}</a>");
                        System.Console.WriteLine("===========3");
                        await _emailSender.SendAsync(new MailMessage(_emailSender.FromAddress, "hero_wong@outlook.com")
                        {
                            Subject = "doghappy 有新评论了",
                            Body = builder.ToString(),
                            IsBodyHtml = true
                        });
                        System.Console.WriteLine("===========4");
                    });
                    return RedirectToAction("Detail", "Article", new { id = dto.ArticleId });
                }
                else
                {
                    TempData["ErrorCaptcha"] = true;
                }
            }
            TempData.Put("ErrorModel", dto);
            return RedirectToAction("Detail", "Article", new { id = dto.ArticleId });
        }

        public IActionResult Captcha()
        {
            return _captcha.GenerateCaptchaImageFileStream(HttpContext.Session);
        }
    }
}