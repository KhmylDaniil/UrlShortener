using System.ComponentModel.DataAnnotations;
using UrlShorter.BLL.Constants;

namespace UrlShorter.BLL.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : EntityBase
    {
        protected User() { }

        public User(string name, string login, string passwordHash, RoleType roleType)
        {
            Name = name;
            Login = login;
            PasswordHash = passwordHash;
            RoleType = roleType;
            UrlRecords = new();
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Тип роли
        /// </summary>
        public RoleType RoleType { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Количество созданных записей
        /// </summary>
        public int UrlRecordsCount { get; set; }

        /// <summary>
        /// Созданные пользователем записи
        /// </summary>
        public List<UrlRecord> UrlRecords { get; set; }
    }
}
