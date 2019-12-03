using System.Collections.Generic;
using System.Threading.Tasks;
using HappyDog.Domain.DataTransferObjects.Tag;
using HappyDog.Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.Console.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        readonly ITagService _tagService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await _tagService.GetTagDtoAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                return new JsonResult(tag);
            }
        }

        [HttpGet]
        public async Task<List<TagDto>> GetTags()
        {
            return await _tagService.GetTagsDtoAsync();
        }

        [HttpPost]
        public async Task<TagDto> Post(PostTagDto dto)
        {
            return await _tagService.PostAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PutTagDto dto)
        {
            var tag = await _tagService.PutAsync(id, dto);
            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                return new JsonResult(tag);
            }
        }
    }
}