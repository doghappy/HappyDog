using HappyDog.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HappyDog.WebUI.ViewComponents
{
    public class PostCommentViewComponent : ViewComponent
    {
        public PostCommentViewComponent(HappyDogContext db)
        {
            _db = db;
        }

        readonly HappyDogContext _db;
        public int test;

        public async Task<IViewComponentResult> InvokeAsync(int articleId)
        {
            var commentCount = await _db.Comments.CountAsync(c => c.ArticleId == articleId);
            ViewBag.ArticleId = articleId;
            return View(commentCount);
        }
    }
}
