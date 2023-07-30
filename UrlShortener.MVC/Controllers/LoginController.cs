using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.MVC.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    public sealed class LoginController : BaseController
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
            catch (Exception ex) { return HandleException<LoginController>(ex, () => View(request)); }
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
            catch (Exception ex) { return HandleException<LoginController>(ex, View); }
        }
    }
}
