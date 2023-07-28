using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Exceptions;
using UrlShorter.BLL.Services;

namespace UrlShorter.BLL
{
    public static class Entry
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            string salt = configuration["Salt"] ??
                            throw new ApplicationSystemBaseException($"Невозможно провести хеширование пароля. Отсутствует параметр salt.");

            services.AddTransient<IPasswordHasher>
                (o => new PasswordHasher(salt));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Entry).Assembly));
        }
    }
}
