using UrlShortener.BLL.Constants;

namespace UrlShortener.BLL.Abstractions
{
    /// <summary>
    /// Сервис проверки авторизации пользователей
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Проверка авторизации пользователей
        /// </summary>
        /// <param name="roleType">Минимальный доступ для действия</param>
        void AuthorizationCheck(RoleType roleType = default);
    }
}
