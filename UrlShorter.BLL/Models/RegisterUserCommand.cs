using MediatR;
using System.ComponentModel.DataAnnotations;

namespace UrlShorter.BLL.Models
{
    /// <summary>
    /// Команда создания пользователя
    /// </summary>
    public class RegisterUserCommand : IRequest<Guid>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [MaxLength(12)]
        public string Name { get; set; }

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

        /// <summary>
        /// Зарегистрировать в качестве админа
        /// </summary>
        public bool AsAdmin { get; set; }
    }
}
