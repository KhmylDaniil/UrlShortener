using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UrlShortener.MVC.Models;

namespace UrlShortener.MVC.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected readonly ISender _sender;

        protected BaseController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Перенаправление на страницу с ошибкой
        /// </summary>
        /// <typeparam name="TController">Контроллер</typeparam>
        /// <param name="ex">Исключение</param>
        /// <returns></returns>
        protected ActionResult RedirectToErrorPage<TController>(Exception ex) where TController : BaseController
        {
            var myLog = Log.ForContext<TController>();
            myLog.Error(ex.Message);
            return View("Error", new ErrorViewModel(ex));
        }
    }
}
