using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.DataTransferObjects.Category;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;

namespace HappyDog.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Article, ArticleDto>();
            CreateMap<Article, ArticleSummaryDto>();
            CreateMap<RegisterDto, User>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<LoginDto, User>();
        }
    }
}
