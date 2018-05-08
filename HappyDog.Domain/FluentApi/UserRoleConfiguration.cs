using HappyDog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyDog.Domain.FluentApi
{
    class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(m => new { m.UserId, m.RoleId });
            builder.HasOne(m => m.User).WithMany(u => u.UserRoles).HasForeignKey(m => m.UserId);
            builder.HasOne(m => m.Role).WithMany(r => r.UserRoles).HasForeignKey(m => m.RoleId);
        }
    }
}
