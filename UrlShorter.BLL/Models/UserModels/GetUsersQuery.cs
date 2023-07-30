using MediatR;

namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Запрос списка пользователей с фильтром
    /// </summary>
    public sealed class GetUserQuery : IRequest<IEnumerable<GetUsersResponse>>
    {
        /// <summary>
        /// Фильтр по имени
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фильтр по минимальному времени регистрации
        /// </summary>
        public DateTime MinRegistrationDateTime { get; set; }

        /// <summary>
        /// Фильтр по максимальному времени регистрации
        /// </summary>
        public DateTime MaxRegistrationDateTime { get; set; }
    }
}
