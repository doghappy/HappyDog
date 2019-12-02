using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Enums;
using HappyDog.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.IServices
{
    public interface IArticleService
    {
        Task<ArticleDetailDto> GetArticleDetailDtoAsync(int id);
        Task<ArticleDetailDto> GetEnabledArticleDetailDtoAsync(int id);
        Task<Pagination<ArticleDto>> GetArticlesDtoAsync(int page, int size, ArticleCategory? cid);
        Task<Pagination<ArticleDto>> SearchAsync(string keyword, int page, int size);
        Task<List<ArticleDto>> GetTopArticlesDtoAsync(int count);
        Task<List<ArticleDto>> GetDisabledArticlesDtoAsync();
        Task<ArticleDetailDto> PostAsync(PostArticleDto dto);
        Task<ArticleDetailDto> PutAsync(int id, PutArticleDto dto);
    }
}
