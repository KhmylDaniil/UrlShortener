using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Entities;

namespace UrlShortener.BLL.Abstractions
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public interface IAppDbContext
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        DbSet<User> Users { get; }

        /// <summary>
        /// Записи в базе данных
        /// </summary>
        DbSet<UrlRecord> UrlRecords { get; }

        /// <summary>
        /// Переопредение метода записи сущностей в базу данных
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
