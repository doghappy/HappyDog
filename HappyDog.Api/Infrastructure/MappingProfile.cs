using AutoMapper;
using HappyDog.DataTransferObjects.Admin;
using HappyDog.DataTransferObjects.Article;
using HappyDog.DataTransferObjects.Category;
using HappyDog.Domain.Entities;
using System;

namespace HappyDog.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<Article, ArticleDTO>();
            CreateMap<Article, ArticleSummaryDTO>();
            CreateMap<RegisterDTO, User>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<LoginDTO, User>();
        }
    }
}
