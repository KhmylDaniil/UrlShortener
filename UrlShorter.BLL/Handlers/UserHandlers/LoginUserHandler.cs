using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.BLL.Handlers.UserHandlers
{
    /// <summary>
    /// Обработчик команды авторизации пользователя
    /// </summary>
    public sealed class LoginUserHandler : BaseHandler<LoginUserCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserHandler(
            IAppDbContext appDbContext,
            IPasswordHasher passwordHasher,
            IHttpContextAccessor httpContextAccessor)
            : base(appDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
        }

        public async override Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _appDbContext.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken)
                ?? throw new RequestValidationException("Логин не найден");

            if (!_passwordHasher.VerifyHash(request.Password, existingUser.PasswordHash))
                throw new RequestValidationException("Неверный пароль.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Role, Enum.GetName(existingUser.RoleType))
            };

            ClaimsIdentity claimsIdentity = new(claims, "Cookies");

            await _httpContextAccessor.HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Unit.Value;
        }
    }
}
