using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Exceptions;

namespace UrlShortener.BLL.Services
{
    /// <summary>
    /// Сервис проверки авторизации пользователей
    /// </summary>
    public sealed class AuthorizationService : IAuthorizationService
    {
        private readonly IUserContext _userContext;

        public AuthorizationService(IUserContext userContext)
        {
            _userContext = userContext;
        }

        /// <inheritdoc/>
        public void AuthorizationCheck(RoleType roleType = default)
        {
            if (!Enum.IsDefined(roleType))
                throw new ApplicationSystemBaseException("Неизвестная роль");

            if (roleType > _userContext.RoleType)
                throw new ApplicationSystemBaseException("Ошибка авторизации, у вас нет прав на проведение данной операции.");
        }
    }
}
