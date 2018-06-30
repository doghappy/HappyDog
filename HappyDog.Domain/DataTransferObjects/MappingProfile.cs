using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.DataTransferObjects.Category;

namespace HappyDog.Domain.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Category, CategoryDto>();
            CreateMap<Entities.Article, ArticleDto>();
            CreateMap<Entities.Article, ArticleSummaryDto>();
            CreateMap<RegisterDto, Entities.User>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<SignInDto, Entities.User>();
        }
    }
}
