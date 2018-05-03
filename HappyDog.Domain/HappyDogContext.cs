using HappyDog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HappyDog.Domain
{
    public class HappyDogContext : DbContext
    {
        public HappyDogContext(DbContextOptions<HappyDogContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
