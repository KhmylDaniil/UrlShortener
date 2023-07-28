using Microsoft.EntityFrameworkCore;
using UrlShorter.BLL.Entities;

namespace UrlShorter.BLL.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }

        DbSet<UrlRecord> UrlRecords { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
