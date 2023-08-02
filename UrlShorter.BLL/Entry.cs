using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Services;

namespace UrlShortener.BLL
{
    public static class Entry
    {
        /// <summary>
        /// Добавление сервисов слоя бизнес-логики
        /// </summary>
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            string salt = configuration["Salt"] ??
                throw new ApplicationSystemBaseException($"Невозможно провести хеширование пароля. Отсутствует параметр salt.");

            services.AddTransient<IPasswordHasher>(o => new PasswordHasher(salt));

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Entry).Assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(Validators.UserModels.LoginUserCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
