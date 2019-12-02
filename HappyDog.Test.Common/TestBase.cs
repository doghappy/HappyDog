using AutoMapper;
using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace HappyDog.Test.Common
{
    public abstract class TestBase
    {
        protected HappyDogContext DbContext { get; private set; }

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


        [TestInitialize]
        public virtual void Initialize()
        {
            DbContext = new HappyDogContext(GetOptions());
        }

        [TestCleanup]
        public virtual async Task CleanupAsync()
        {
            await DbContext.DisposeAsync();
        }
    }
}
