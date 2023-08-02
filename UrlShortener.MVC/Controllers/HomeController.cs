using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Models.UrlModels;
using UrlShortener.MVC.Models;

namespace UrlShortener.MVC.Controllers
{
    /// <summary>
    /// Контроллер главной страницы
    /// </summary>
    public sealed class HomeController : BaseController
    {
        private readonly CustomHttpClient _httpClient;
        private readonly IUserContext _userContext;

        public HomeController(ISender sender, CustomHttpClient httpClient, IUserContext userContext) : base(sender)
        {
            _httpClient = httpClient;
            _userContext = userContext;
        }

        /// <summary>
        /// Вход на главную страницу
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (_userContext.RoleType == BLL.Constants.RoleType.Admin)
                ViewData["IsAdmin"] = true;

            return View();
        }

        /// <summary>
        /// Получением длинного url для сокращения
        /// </summary>
        /// <param name="request">Команда создания короткого url</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateUrlRecordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _httpClient.HealthCheckAsync(request.LongUrl, cancellationToken);

                var newUrl = await _sender.Send(request, cancellationToken);
                ViewData["NewShortUrl"] = BLL.Constants.BaseAddress.RedirectHttpAddress + newUrl;

                return View();
            }
            catch (Exception ex) { return HandleException<HomeController>(ex, () => View(request)); }
        }

        /// <summary>
        /// Перенаправление ошибки из api контроллера в  mvc для автоматического создания razor page
        /// </summary>
        /// <returns></returns>
        public IActionResult RedirectFromApiError()
            => View("Error", new ErrorViewModel(new EntityNotFoundException<UrlRecord>()));
    }
}
