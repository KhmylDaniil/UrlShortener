using UrlShortener.BLL.Constants;

namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Элемент ответа на запрос списка пользователей
    /// </summary>
    public class GetUsersResponse
    {
        /// <summary>
        /// Идентефикатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDateTime { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; set; }
    }
}
