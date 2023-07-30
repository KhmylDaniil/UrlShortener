using MediatR;

namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Команда на изменение пользователя
    /// </summary>
    public sealed class EditUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Смена роли
        /// </summary>
        public bool SwitchRole { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        public string? SetNewPassword { get; set; }
    }
}
