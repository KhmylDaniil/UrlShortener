using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UrlShortener.BLL.Models.UrlModels;

namespace UrlShortener.MVC.Controllers
{
    /// <summary>
    /// Контроллер для перенаправления поступившего короткого url на длинный
    /// </summary>
    [Route("api/[controller]/")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly ISender _sender;

        public UrlController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Метод перенаправления поступившего короткого url на длинный
        /// </summary>
        /// <param name="shortUrl">Короткий url</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<IActionResult> RedirectToLongUrl(string shortUrl, CancellationToken cancellationToken)
        {
            GetLongUrlQuery query = new(shortUrl);

            try
            {
                var longUrl = await _sender.Send(query, cancellationToken);

                return Redirect(longUrl);
            }
            catch (Exception ex)
            {
                var myLog = Log.ForContext<UrlController>();
                myLog.Error(ex.Message);

                return RedirectToAction("RedirectFromApiError", "Home");
            }
        }
    }
}
