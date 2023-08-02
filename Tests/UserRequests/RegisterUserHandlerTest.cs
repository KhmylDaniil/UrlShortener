using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Handlers.UserHandlers;
using UrlShortener.BLL.Models.UserModels;

namespace Tests.UserRequests
{
    /// <summary>
    /// Тестовый класс для проверки команды регистрации пользователя
    /// </summary>
    [TestClass]
    public class RegisterUserHandlerTest : UnitTestBase
    {
        private readonly IAppDbContext _appDbContext;
        
        public RegisterUserHandlerTest()
        {
            _appDbContext = CreateInMemoryContext();
        }

        /// <summary>
        /// Метод дял проверки команды регистрации пользователя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="asAdmin">Зарегистрировать как админа</param>
        /// <returns></returns>
        [DataRow("name1", "password1", "login1", false)]
        [DataRow("name2", "password2", "login2", true)]
        [TestMethod]
        public async Task Handle_RegisterUserCommandHandler_ShouldRegisterUser(string name, string login, string password, bool asAdmin)
        {
            var request = new RegisterUserCommand()
            {
                Name = name,
                Login = login,
                Password = password,
                AsAdmin = asAdmin
            };

            var handler = new RegisterUserHandler(_appDbContext, PasswordHasher.Object);

            var result = await handler.Handle(request, default);

            Assert.IsNotNull(result);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Login == login);

            Assert.AreEqual(request.Name, user.Name);
            Assert.AreEqual(request.Login, user.Login);
            Assert.AreEqual(request.AsAdmin, user.RoleType == RoleType.Admin);
        }

        /// <summary>
        /// Метод проверки исключения при попытке регистрации пользователя с неуникальным логином
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_RegisterUserCommandHandler_ShouldNotRegisterUserWithSameLogin()
        {
            var request = new RegisterUserCommand()
            {
                Name = "test1",
                Login = "login",
                Password = "password1"
            };

            var handler = new RegisterUserHandler(_appDbContext, PasswordHasher.Object);

            var result = await handler.Handle(request, default);

            Assert.IsNotNull(result);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Login == request.Login);
            Assert.IsNotNull(user);

            request = new RegisterUserCommand()
            {
                Name = "test2",
                Login = "login",
                Password = "password2"
            };

            await Assert.ThrowsExceptionAsync<RequestValidationException>(
                action: async () => await handler.Handle(request, default),
                message: ExceptionMessages.NotUniqueLogin);
        }
    }
}
