using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UrlShorter.BLL.Entities;

namespace UrlShortener.DAL.Configurations
{
    public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureBase(builder);
            ConfigureChild(builder);
        }

        public virtual void ConfigureBase(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(r => r.Id).IsRequired();
            builder.Property(r => r.CreatedOn).IsRequired();
            builder.Property(r => r.CreatedByUserId).IsRequired();
        }

        public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
    }
}
