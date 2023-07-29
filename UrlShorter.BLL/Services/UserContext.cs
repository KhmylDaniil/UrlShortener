using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Constants;
using UrlShorter.BLL.Exceptions;

namespace UrlShorter.BLL.Services
{
    /// <summary>
    /// Сервис доступа к идентификатору и роли текущего пользователя
    /// </summary>
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public Guid CurrentUserId
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    return Guid.Empty;

                var value = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value
                    ?? throw new ApplicationSystemBaseException("Айди текущего пользователя не определено");

                return new Guid(value);
            }
        }

        /// <inheritdoc/>
        public RoleType RoleType
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    return RoleType.Guest;

                var value = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                    ?? throw new ApplicationSystemBaseException("Роль текущего пользователя не определена");

                return Enum.Parse<RoleType>(value);
            }
        }
    }
}
