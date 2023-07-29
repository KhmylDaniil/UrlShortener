using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.MVC.Controllers
{
    /// <summary>
    /// Контроллер главной страницы
    /// </summary>
    public sealed class HomeController : BaseController
    {
        public HomeController(ISender sender) : base(sender) { }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
