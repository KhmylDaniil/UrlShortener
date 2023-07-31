using MediatR;

namespace UrlShortener.BLL.Models.UrlModels
{
    /// <summary>
    /// Запрос на получение длинного Url из базы данных
    /// </summary>
    public sealed record GetLongUrlQuery : IRequest<string>
    {
        /// <summary>
        /// Короткий Url
        /// </summary>
        public string ShortUrl { get; init; }

        public GetLongUrlQuery(string url) => ShortUrl = url;
    }
}
