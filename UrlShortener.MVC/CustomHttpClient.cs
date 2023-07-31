using System.Net;
using UrlShortener.BLL.Exceptions;

namespace UrlShortener.MVC
{
    public class CustomHttpClient
    {
        private readonly HttpClient _httpClient;

        public CustomHttpClient(HttpClient httpClient) => _httpClient = httpClient;

        /// <summary>
        /// Обращение к внешнему API
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public async Task HealthCheckAsync(string uri, CancellationToken cancellationToken)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                throw new NotValidLongUrlException();

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new NotValidLongUrlException();
        }
    }
}
