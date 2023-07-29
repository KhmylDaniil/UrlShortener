using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.BLL.Entities;

namespace UrlShortener.DAL.Configurations
{
    /// <summary>
    /// Конфигурация записей Url
    /// </summary>
    public class UrlRecordConfiguration : EntityBaseConfiguration<UrlRecord>
    {
        public override void ConfigureChild(EntityTypeBuilder<UrlRecord> builder)
        {
            builder.Property(x => x.ShortUrl).IsRequired();

            builder.Property(x => x.LongUrl).IsRequired();

            builder.HasMany(ur => ur.Users)
                .WithMany(u => u.UrlRecords)
                .UsingEntity(x => x.ToTable("UsersUrlRecords"));
        }
    }
}
