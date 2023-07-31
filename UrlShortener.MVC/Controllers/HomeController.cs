using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.Models.UrlModels;

namespace UrlShortener.MVC.Controllers
{
    /// <summary>
    /// Контроллер главной страницы
    /// </summary>
    public sealed class HomeController : BaseController
    {
        private readonly CustomHttpClient _httpClient;
        public HomeController(ISender sender, CustomHttpClient httpClient) : base(sender)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateUrlRecordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _httpClient.HealthCheckAsync(request.LongUrl, cancellationToken);

                var newUrl = await _sender.Send(request, cancellationToken);
                ViewData["NewShortUrl"] = BLL.Constants.BaseAddress.HttpAddress + newUrl;

                return View();
            }
            catch (Exception ex) { return HandleException<HomeController>(ex, () => View(request)); }
        }
    }
}
