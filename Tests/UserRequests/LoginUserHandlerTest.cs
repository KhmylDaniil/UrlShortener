using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Handlers.UserHandlers;
using UrlShortener.BLL.Models.UserModels;

namespace Tests.UserRequests
{
    /// <summary>
    /// Тестовый класс для проверки команды авторизации пользователя
    /// </summary>
    [TestClass]
    public class LoginUserHandlerTest : UnitTestBase
    {
        private readonly IAppDbContext _appDbContext;

        private readonly User _user;

        public LoginUserHandlerTest()
        {
            _user = new(
                name: "user",
                login: "login",
                passwordHash: "foo",
                roleType: RoleType.Admin);

            _appDbContext = CreateInMemoryContext(x => x.AddRange(_user));
        }

        /// <summary>
        /// Метод проверки правильной авторизации пользователя
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_LoginUserHandler_ShouldLoginUser()
        {
            var request = new LoginUserCommand()
            {
                Login = "login",
                Password = "anything"
            };

            var handler = new TestLoginUserCommandHandler(_appDbContext, PasswordHasher.Object, new Mock<IHttpContextAccessor>().Object);

            var result = await handler.Handle(request, default);

            Assert.IsNotNull(result);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Login == request.Login);
            Assert.IsNotNull(user);
        }

        /// <summary>
        /// Метод проверки исключения при попытке ввода неправильного логина
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_LoginUserHandler_ShouldNotLoginUserWithIncorrectLogin()
        {
            var request = new LoginUserCommand()
            {
                Login = "login2",
                Password = "anything"
            };

            var handler = new TestLoginUserCommandHandler(_appDbContext, PasswordHasher.Object, new Mock<IHttpContextAccessor>().Object);

            await Assert.ThrowsExceptionAsync<RequestValidationException>(
                action: async () => await handler.Handle(request, default),
                message: ExceptionMessages.NotUniqueLogin);
        }
    }

    /// <summary>
    /// Тестовый класс для обработчика
    /// </summary>
    class TestLoginUserCommandHandler : LoginUserHandler
    {
        public TestLoginUserCommandHandler(IAppDbContext appDbContext, IPasswordHasher passwordHasher, IHttpContextAccessor httpContextAccessor)
            : base(appDbContext, passwordHasher, httpContextAccessor)
        {
        }

        /// <summary>
        /// Переопределенный метод для исключения из тестирования метода расширения
        /// </summary>
        protected override async Task SignCookiesAsync(HttpContext httpContext, ClaimsPrincipal claimsPrincipal)
        {
            await Task.CompletedTask;
        }
    }
}
