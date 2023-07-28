using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShorter.BLL.Entities;

namespace UrlShortener.DAL.Configurations
{
    public class UrlRecordConfiguration : EntityBaseConfiguration<UrlRecord>
    {
        public override void ConfigureChild(EntityTypeBuilder<UrlRecord> builder)
        {
            builder.Property(x => x.ShortUrl).IsRequired();

            builder.Property(r => r.LongUrl).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.UrlRecords)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
