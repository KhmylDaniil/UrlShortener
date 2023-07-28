using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShorter.BLL.Entities;

namespace UrlShortener.DAL.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public override void ConfigureChild(EntityTypeBuilder<User> builder)
        {
            builder.Property(r => r.Name).IsRequired();

            builder.Property(r => r.PasswordHash).IsRequired();

            builder.HasMany(x => x.UrlRecords)
                .WithOne(x => x.User)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
