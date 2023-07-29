using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISender sender) : base(sender) { }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
