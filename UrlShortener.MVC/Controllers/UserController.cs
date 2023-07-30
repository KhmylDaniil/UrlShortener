using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.MVC.Controllers
{
    /// <summary>
    /// Контроллер методов работы с пользователями
    /// </summary>
    public sealed class UserController : BaseController
    {
        public UserController(ISender sender) : base(sender) { }

        public async Task<IActionResult> Index(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _sender.Send(request, cancellationToken);

                return View(response);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync<UserController>(ex, async () =>
                    View(await _sender.Send(new GetUserQuery(), cancellationToken)));
            }
        }

        public async Task<IActionResult> Details(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _sender.Send(request, cancellationToken);

                return View(response);
            }
            catch (Exception ex) 
            {
                return await HandleExceptionAsync<UserController>(ex, async () =>
                    View(await _sender.Send(new GetUserQuery(), cancellationToken)));
            }
        }

        public ActionResult Edit(EditUserCommand command) => View(command);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _sender.Send(command, cancellationToken);

                return RedirectToAction(nameof(Details), new GetUserByIdQuery() { Id = command.Id });
            }
            catch (Exception ex) { return HandleException<UserController>(ex, () => View(command)); }
        }

        public ActionResult Delete(DeleteUserCommand command) => View(command);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _sender.Send(command, cancellationToken);

                return RedirectToAction(nameof(Index), new GetUserQuery());
            }
            catch (Exception ex) { return HandleException<UserController>(ex, () => View(command)); }
        }
    }
}
