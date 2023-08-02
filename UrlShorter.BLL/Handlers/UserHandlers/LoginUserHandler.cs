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
    public class LoginUserHandler : BaseHandler<LoginUserCommand, Unit>
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
                ?? throw new RequestValidationException(Constants.ExceptionMessages.LoginInccorrect);

            if (!_passwordHasher.VerifyHash(request.Password, existingUser.PasswordHash))
                throw new RequestValidationException(Constants.ExceptionMessages.PasswordInccorrect);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Role, Enum.GetName(existingUser.RoleType))
            };

            ClaimsIdentity claimsIdentity = new(claims, "Cookies");

            await SignCookiesAsync(_httpContextAccessor.HttpContext, new ClaimsPrincipal(claimsIdentity));

            return Unit.Value;
        }

        /// <summary>
		/// Выделение метода расширения для возможности переопределения его поведения в тестах 
		/// </summary>
		protected virtual async Task SignCookiesAsync(HttpContext httpContext, ClaimsPrincipal claimsPrincipal)
            => await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }
}
