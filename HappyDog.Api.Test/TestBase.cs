using AutoMapper;
using AutoMapper.Configuration;
using HappyDog.Api.Infrastructure;
using HappyDog.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace HappyDog.Api.Test
{
    public class TestBase
    {
        public TestBase()
        {
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfile<MappingProfile>();
            AutoMapper.Mapper.Initialize(mappings);
            Mapper= AutoMapper.Mapper.Instance;
        }

        protected IMapper Mapper { get; }

        protected DbContextOptions<HappyDogContext> GetOptions()
        {
            return new DbContextOptionsBuilder<HappyDogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        protected DbContextOptions<HappyDogContext> GetOptions(string dbName)
        {
            return new DbContextOptionsBuilder<HappyDogContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }
    }
}
