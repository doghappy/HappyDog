using AutoMapper;
using AutoMapper.Configuration;
using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace HappyDog.WebUI.Test
{
    public class TestBase
    {
        static IMapper mapper;
        protected IMapper Mapper
        {
            get
            {
                if (mapper == null)
                {
                    var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
                    mapper = mappingConfig.CreateMapper();
                }
                return mapper;
            }
        }

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
