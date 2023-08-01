using MediatR;

namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Команда создания пользователя
    /// </summary>
    public sealed class RegisterUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Логин
        /// </summary>
        public string? Login { get; init; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string? Password { get; init; }

        /// <summary>
        /// Зарегистрировать в качестве админа
        /// </summary>
        public bool AsAdmin { get; init; }
    }
}
