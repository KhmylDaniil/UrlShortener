
namespace UrlShortener.BLL.Abstractions
{
    /// <summary>
    /// Провайдер времени
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Текущее время
        /// </summary>
        public DateTime Now { get; }
    }
}
