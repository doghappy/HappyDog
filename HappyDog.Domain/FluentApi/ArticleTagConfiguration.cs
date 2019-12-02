using HappyDog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyDog.Domain.FluentApi
{
    class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(m => new { m.ArticleId, m.TagId });
            builder.HasOne(m => m.Article).WithMany(a => a.ArticleTags).HasForeignKey(m => m.ArticleId);
            builder.HasOne(m => m.Tag).WithMany(t => t.ArticleTags).HasForeignKey(m => m.TagId);
        }
    }
}
