using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

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

        public IViewComponentResult Invoke(int articleId)
        {
            ViewBag.ArticleId = articleId;
            return View(TempData.Get<PostCommentDto>("ErrorModel"));
        }
    }
}
