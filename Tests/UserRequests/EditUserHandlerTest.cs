using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Handlers.UserHandlers;
using UrlShortener.BLL.Models.UserModels;

namespace Tests.UserRequests
{
    /// <summary>
    /// Тестовый класс для проверки команды изменения пользователя
    /// </summary>
    [TestClass]
    public class EditUserHandlerTest : UnitTestBase
    {
        private readonly User _user;
        private readonly IAppDbContext _appDbContext;

        public EditUserHandlerTest()
        {
            _user = new("user1", "login1", "notFoo", RoleType.User);
            _appDbContext = CreateInMemoryContext(x => x.AddRange(_user));
        }

        /// <summary>
        /// Метод проверки команды изменения пользователя
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_EditUserHandler()
        {
            var request = new EditUserCommand()
            {
                Id = _user.Id,
                Name = "newName",
                SwitchRole = true,
                SetNewPassword = "anything"
            };

            var handler = new EditUserHandler(_appDbContext, AuthorizationService.Object, PasswordHasher.Object);

            var result = await handler.Handle(request, default);

            Assert.IsNotNull(result);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            Assert.IsNotNull(user);

            Assert.AreEqual(request.Name, user.Name);
            Assert.AreEqual(RoleType.Admin, user.RoleType);
            Assert.AreEqual("foo", user.PasswordHash);
        }
    }
}
