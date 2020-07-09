using System.Threading.Tasks;
using HappyDog.Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class TagController : Controller
    {
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        readonly ITagService _tagService;

        public async Task<IActionResult> List()
        {
            var tags = await _tagService.GetTagsDtoAsync();
            return View(tags);
        }

        [Route("Tag/{name}/Article")]
        public async Task<IActionResult> Article(string name, int page = 1)
        {
            var pagination = await _tagService.GetArticlesDtoAsync(name, page, 20);
            if (pagination == null || pagination.Data.Count < 1)
            {
                return NotFound();
            }
            else
            {
                return View(pagination);
            }
        }
    }
}
