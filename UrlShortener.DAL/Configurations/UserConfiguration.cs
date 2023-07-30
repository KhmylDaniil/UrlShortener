using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.BLL.Entities;

namespace UrlShortener.DAL.Configurations
{
    /// <summary>
    /// Конфигурация пользователей
    /// </summary>
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public override void ConfigureChild(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.RoleType).IsRequired();

            builder.Property(x => x.Login).IsRequired();

            builder.Property(x => x.PasswordHash).IsRequired();

            builder.Property(x => x.UrlRecordsCount).IsRequired();

            builder.HasMany(u => u.UrlRecords)
                .WithMany(ur => ur.Users)
                .UsingEntity(x => x.ToTable("UsersUrlRecords"));
        }
    }
}
