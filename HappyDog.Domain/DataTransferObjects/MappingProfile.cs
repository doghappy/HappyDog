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
            CreateMap<Entities.Article, ArticleDetailDto>();
            CreateMap<PostArticleDto, Entities.Article>();
            CreateMap<SignUpDto, Entities.User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<SignInDto, Entities.User>();
        }
    }
}
