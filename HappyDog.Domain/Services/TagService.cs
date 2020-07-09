using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.DataTransferObjects.Tag;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.IServices;
using HappyDog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class TagService : ITagService
    {
        public TagService(HappyDogContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        readonly HappyDogContext _db;
        readonly IMapper _mapper;

        public async Task<TagDto> PostAsync(PostTagDto dto)
        {
            var tag = _mapper.Map<Tag>(dto);
            await _db.Tags.AddAsync(tag);
            await _db.SaveChangesAsync();
            return _mapper.Map<TagDto>(tag);
        }

        public async Task<TagDto> GetTagDtoAsync(int id)
        {
            var tag = await _db.Tags
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);
            return _mapper.Map<TagDto>(tag);
        }

        public async Task<Pagination<ArticleDto>> GetArticlesDtoAsync(string name, int page, int size)
        {
            var tag = await _db.Tags
                .AsNoTracking()
                .Include(t => t.ArticleTags)
                .SingleOrDefaultAsync(t => t.Name == name);
            if (tag == null)
            {
                return default;
            }
            else
            {
                var articleIds = tag.ArticleTags.Select(at => at.ArticleId).ToList();
                var query = _db.Articles
                    .AsNoTracking()
                    .Where(a => a.Status == BaseStatus.Enabled && articleIds.Contains(a.Id));
                var pagination = new Pagination<ArticleDto>
                {
                    Page = page,
                    Size = size,
                    TotalItems = await query.CountAsync(),
                };
                pagination.Data = await query
                    .OrderByDescending(a => a.Id)
                    .Skip((page - 1) * size)
                    .Take(size)
                    .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                    .Include(a => a.Category)
                    .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return pagination;
            }
        }

        public async Task<List<TagDto>> GetTagsDtoAsync()
        {
            return await _db.Tags
                .AsNoTracking()
                .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<TagDto>> SearchTagsDtoAsync(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return await GetTagsDtoAsync();
            }
            else
            {
                return await _db.Tags
                    .AsNoTracking()
                    .Where(t => EF.Functions.Like(t.Name, $"%{q}%"))
                    .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }

        public async Task<TagDto> PutAsync(int id, PutTagDto dto)
        {
            var tag = await _db.Tags.FindAsync(id);
            if (tag == null)
            {
                return default;
            }
            else
            {
                tag.Name = dto.Name;
                tag.Color = dto.Color;
                tag.Glyph = dto.Glyph;
                tag.GlyphFont = dto.GlyphFont;
                await _db.SaveChangesAsync();
                return _mapper.Map<TagDto>(tag);
            }
        }
    }
}
