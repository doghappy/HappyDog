using System.Threading.Tasks;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using HappyDog.WebUI.Extensions;
using Edi.Captcha;

namespace HappyDog.WebUI.Controllers
{
    public class CommentController : Controller
    {
        public CommentController(
            CommentService commentService,
            ISessionBasedCaptcha captcha)
        {
            _commentService = commentService;
            _captcha = captcha;
        }

        readonly CommentService _commentService;
        readonly ISessionBasedCaptcha _captcha;

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(PostCommentDto dto)
        {
            if (ModelState.IsValid)
            {
                if (_captcha.ValidateCaptchaCode(dto.Code, HttpContext.Session))
                {
                    dto.IPv4 = Request.Host.ToString();
                    await _commentService.CreateAsync(dto);
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