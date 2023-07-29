using MediatR;
using System.ComponentModel.DataAnnotations;

namespace UrlShorter.BLL.Models
{
    /// <summary>
    /// Команда на изменение пользователя
    /// </summary>
    public class EditUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Name { get; set; }

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
