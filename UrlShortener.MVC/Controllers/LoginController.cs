using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShorter.BLL.Exceptions;
using UrlShorter.BLL.Models;

namespace UrlShortener.MVC.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(ISender sender) : base(sender) { }

        public IActionResult Index() => View();

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _sender.Send(request, cancellationToken);

                await _sender.Send(new LoginUserCommand() { Login = request.Login, Password = request.Password }, cancellationToken);

                return RedirectToAction("Index", "Home");
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            catch (Exception ex) { return RedirectToErrorPage<LoginController>(ex); }
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _sender.Send(request, cancellationToken);

                return RedirectToAction("Index", "Home");
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            catch (Exception ex) { return RedirectToErrorPage<LoginController>(ex); }
        }
    }
}
