using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HappyDog.Console.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        public ArticleController(HappyDogContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        readonly HappyDogContext _db;
        readonly IMapper _mapper;

        [HttpGet("disabled")]
        public async Task<List<ArticleDto>> DisabledList()
        {
            return await _db.Articles.AsNoTracking()
                .Include(a => a.Category)
                .Where(a => a.Status == BaseStatus.Disable)
                .OrderByDescending(a => a.Id)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ArticleDetailDto> Post([FromBody]PostArticleDto dto)
        {
            var article = _mapper.Map<Article>(dto);
            article.CreateTime = DateTimeOffset.Now;
            await _db.Articles.AddAsync(article);
            await _db.SaveChangesAsync();
            return _mapper.Map<ArticleDetailDto>(article);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PutArticleDto dto)
        {
            var article = await _db.Articles.SingleOrDefaultAsync(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                article.CategoryId = dto.CategoryId;
                article.Title = dto.Title;
                article.Content = dto.Content;
                article.Status = dto.Status;
                await _db.SaveChangesAsync();
                return new JsonResult(_mapper.Map<ArticleDetailDto>(article));
            }
        }
    }
}