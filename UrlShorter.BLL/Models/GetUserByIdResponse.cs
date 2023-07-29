
namespace UrlShorter.BLL.Models
{
    /// <summary>
    /// Ответ на запрос пользователя по идентефикатору
    /// </summary>
    public class GetUserByIdResponse : GetUsersResponse
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Количество созданных записей
        /// </summary>
        public int UrlRecordsCount { get; set; }
    }
}
