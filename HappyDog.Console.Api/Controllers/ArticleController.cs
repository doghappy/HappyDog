using System.Collections.Generic;
using System.Threading.Tasks;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.Console.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        readonly IArticleService _articleService;

        [HttpGet("disabled")]
        public async Task<List<ArticleDto>> GetDisabledList()
        {
            return await _articleService.GetDisabledArticlesDtoAsync();
        }

        [HttpPost]
        public async Task<ArticleDetailDto> Post([FromBody]PostArticleDto dto)
        {
            return await _articleService.PostAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PutArticleDto dto)
        {
            var article = await _articleService.PutAsync(id, dto);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                return new JsonResult(article);
            }
        }

        [HttpGet("{id}")]
        public async Task<ArticleDetailDto> Detail(int id)
        {
            return await _articleService.GetArticleDetailDtoAsync(id);
        }
    }
}