using System.ComponentModel.DataAnnotations;

namespace UrlShortener.BLL.Entities
{
    /// <summary>
    /// Запись Url в базе данных
    /// </summary>
    public sealed class UrlRecord : EntityBase
    {
        /// <summary>
        /// Короткая запись
        /// </summary>
        [StringLength(6)]
        public string ShortUrl { get; set; }

        /// <summary>
        /// Длинная запись
        /// </summary>
        public string LongUrl { get; set; }

        /// <summary>
        /// Пользователи, запросившие создание данной записи 
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
