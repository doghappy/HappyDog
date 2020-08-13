using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.DataTransferObjects.Tag;
using HappyDog.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.IServices
{
    public interface ITagService
    {
        Task<TagDto> PostAsync(PostTagDto dto);
        Task<TagDto> GetTagDtoAsync(int id);
        Task<Pagination<ArticleDto>> GetArticlesDtoAsync(string tagName, int page, int size);
        Task<List<TagDto>> GetTagsDtoAsync();
        Task<List<TagDto>> SearchTagsDtoAsync(string q);
        Task<TagDto> PutAsync(int id, PutTagDto dto);
    }
}
