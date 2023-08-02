using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Handlers.UserHandlers;
using UrlShortener.BLL.Models.UserModels;

namespace Tests.UserRequests
{
    /// <summary>
    /// Тестовый класс для проверки команды удаления пользователя
    /// </summary>
    [TestClass]
    public class DeleteUserHandlerTest : UnitTestBase
    {
        private readonly User _user;
        private readonly IAppDbContext _appDbContext;

        public DeleteUserHandlerTest()
        {
            _user = new("user1", "login1", "notFoo", RoleType.User);
            _appDbContext = CreateInMemoryContext(x => x.AddRange(_user));
        }

        /// <summary>
        /// Метод проверки команды удвления пользователя
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_DeleteUserHandler()
        {
            var request = new DeleteUserCommand()
            {
                Id = _user.Id
            };

            var handler = new DeleteUserHandler(_appDbContext, AuthorizationService.Object);

            var result = await handler.Handle(request, default);

            Assert.IsNotNull(result);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            Assert.IsNull(user);
        }
    }
}
