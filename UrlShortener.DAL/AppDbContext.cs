using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Entities;

namespace UrlShortener.DAL
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UrlRecord> UrlRecords { get; set; }

        private readonly IUserContext _userContext;

        public AppDbContext(
            DbContextOptions<AppDbContext> dbContextOptions,
            IUserContext userContext)
            : base(dbContextOptions)
        {
            _userContext = userContext;
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is EntityBase && e.State == EntityState.Added);

            foreach (var entityEntry in entries)
                if (entityEntry.Entity is EntityBase entityBase)
                {
                    entityBase.Id = Guid.NewGuid();
                    entityBase.CreatedByUserId = _userContext.CurrentUserId;
                    entityBase.CreatedOn = DateTime.UtcNow;
                }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
