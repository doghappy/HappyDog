using HappyDog.Domain.Services;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HappyDog.WebUI.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        public CommentsViewComponent(CommentService commentService)
        {
            _commentService = commentService;
        }

        readonly CommentService _commentService;

        public async Task<IViewComponentResult> InvokeAsync(int articleId)
        {
            var comments = await _commentService.GetCommentDtosAsync(articleId);
            foreach (var item in comments)
            {
                var pipeline = new MarkdownPipelineBuilder()
                    .UsePipeTables()
                    .Build();
                item.Content = Markdown.ToHtml(item.Content, pipeline);
            }
            return View(comments);
        }
    }
}
