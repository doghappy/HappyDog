using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.DataTransferObjects.Category;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.DataTransferObjects.Tag;
using System.Linq;

namespace HappyDog.Domain.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Category, CategoryDto>();
            CreateMap<Entities.Article, ArticleDto>()
                .ForMember(dto => dto.Tags, opt => opt.MapFrom(at => at.ArticleTags.Select(i => i.Tag)));
            CreateMap<Entities.Article, ArticleDetailDto>()
                .ForMember(dto => dto.Tags, opt => opt.MapFrom(at => at.ArticleTags.Select(i => i.Tag)));
            CreateMap<PostArticleDto, Entities.Article>();
            CreateMap<SignUpDto, Entities.User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<SignInDto, Entities.User>();
            CreateMap<PostCommentDto, Entities.Comment>();
            CreateMap<Entities.Comment, CommentDto>();
            CreateMap<PostTagDto, Entities.Tag>();
            CreateMap<Entities.Tag, TagDto>();
        }
    }
}
