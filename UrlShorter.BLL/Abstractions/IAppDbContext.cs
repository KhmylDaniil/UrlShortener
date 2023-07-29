using Microsoft.EntityFrameworkCore;
using UrlShorter.BLL.Entities;

namespace UrlShorter.BLL.Abstractions
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
