using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UrlShortener.BLL.Exceptions;
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

        /// <summary>
        /// Метод обработки ислючений
        /// </summary>
        /// <typeparam name="TController">Контроллер</typeparam>
        /// <param name="ex">Исключение</param>
        /// <param name="validationErrorBehavior">Поведение при ошибке валидации</param>
        /// <returns></returns>
        protected ActionResult HandleException<TController>(Exception ex, Func<ActionResult> validationErrorBehavior)
            where TController : BaseController
        {
            switch (ex)
            {
                case ValidationException:
                    return validationErrorBehavior.Invoke();
                case RequestValidationException valEx:
                    TempData["ErrorMessage"] = valEx.Message;
                    return validationErrorBehavior.Invoke();
                default:
                    var myLog = Log.ForContext<TController>();
                    myLog.Error(ex.Message);
                    return View("Error", new ErrorViewModel(ex));
            }
        }

        /// <summary>
        /// Асинхронный метод обработки ислючений
        /// </summary>
        /// <typeparam name="TController">Контроллер</typeparam>
        /// <param name="ex">Исключение</param>
        /// <param name="validationErrorBehavior">Поведение при ошибке валидации</param>
        /// <returns></returns>
        protected async Task<IActionResult> HandleExceptionAsync<TController>(Exception ex, Func<Task<IActionResult>> validationErrorBehavior)
            where TController : BaseController
        {
            switch (ex)
            {
                case ValidationException:
                    return await validationErrorBehavior.Invoke();
                case RequestValidationException valEx:
                    TempData["ErrorMessage"] = valEx.Message;
                    return await validationErrorBehavior.Invoke();
                default:
                    var myLog = Log.ForContext<TController>();
                    myLog.Error(ex.Message);
                    return View("Error", new ErrorViewModel(ex));
            }
        }
    }
}
