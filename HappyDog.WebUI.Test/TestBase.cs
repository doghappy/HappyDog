using HappyDog.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace HappyDog.WebUI.Test
{
    public class TestBase
    {
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
