using HappyDog.Domain.Entities;
using HappyDog.Domain.FluentApi;
using Microsoft.EntityFrameworkCore;

namespace HappyDog.Domain
{
    public class HappyDogContext : DbContext
    {
        public HappyDogContext(DbContextOptions<HappyDogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleTagConfiguration());
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTagMappings { get; set; }
    }
}
