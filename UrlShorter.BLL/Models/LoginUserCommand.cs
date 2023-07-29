using MediatR;
using System.ComponentModel.DataAnnotations;

namespace UrlShorter.BLL.Models
{
    /// <summary>
    /// Команда авторизации пользователя
    /// </summary>
    public class LoginUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        [MaxLength(12)]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
