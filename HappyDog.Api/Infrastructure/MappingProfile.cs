using AutoMapper;
using HappyDog.DataTransferObjects.Article;
using HappyDog.DataTransferObjects.Category;
using HappyDog.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using System;

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
