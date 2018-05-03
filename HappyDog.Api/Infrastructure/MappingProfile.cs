using AutoMapper;
using HappyDog.DataTransferObjects.Article;
using HappyDog.DataTransferObjects.Category;
using HappyDog.Domain.Entities;

namespace HappyDog.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<Article, ArticleDTO>();
            CreateMap<Article, ArticleSummaryDTO>();
        }
    }
}
