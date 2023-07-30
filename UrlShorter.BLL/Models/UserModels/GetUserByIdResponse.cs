namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Ответ на запрос пользователя по идентефикатору
    /// </summary>
    public sealed class GetUserByIdResponse : GetUsersResponse
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
