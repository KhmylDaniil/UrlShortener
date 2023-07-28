using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Exceptions;

namespace UrlShorter.BLL.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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
    }
}
