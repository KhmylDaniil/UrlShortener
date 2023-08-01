using MediatR;

namespace UrlShortener.BLL.Models.UrlModels
{
    /// <summary>
    /// Запрос на создание новой короткой записи
    /// </summary>
    public sealed record CreateUrlRecordCommand : IRequest<string>
    {
        /// <summary>
        /// Длинный Url
        /// </summary>
        public string LongUrl { get; init; }
    }
}
