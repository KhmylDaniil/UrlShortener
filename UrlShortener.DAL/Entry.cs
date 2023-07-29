using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Exceptions;

namespace UrlShortener.DAL
{
    public static class Entry
    {
        /// <summary>
        /// Метод расширения для подключение базы данных
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddSqlStorage
            (this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddTransient<IAppDbContext, AppDbContext>();

            return services;
        }

        /// <summary>
        /// Автоматическая проверка миграций базы данных
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void MigrateDB(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<AppDbContext>()
                ?? throw new ApplicationSystemBaseException("Невозможно создать контекст базы данных");

            dbContext.Database.Migrate();
        }
    }
}