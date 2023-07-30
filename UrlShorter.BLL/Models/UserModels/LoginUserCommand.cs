using MediatR;

namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Команда авторизации пользователя
    /// </summary>
    public sealed class LoginUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string? Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string? Password { get; set; }
    }
}
