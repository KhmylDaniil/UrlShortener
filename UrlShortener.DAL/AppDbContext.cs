using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;

namespace UrlShortener.DAL
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class AppDbContext : DbContext, IAppDbContext
    {
        /// <inheritdoc/>
        public DbSet<User> Users { get; set; }

        /// <inheritdoc/>
        public DbSet<UrlRecord> UrlRecords { get; set; }

        /// <inheritdoc/>
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

        /// <summary>
        /// Метод для подтягивания конфигураций
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        /// <summary>
        /// Переопределенный метод записи в базу для автоматической записи полей в новых сущностях
        /// </summary>
        /// <returns></returns>
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
