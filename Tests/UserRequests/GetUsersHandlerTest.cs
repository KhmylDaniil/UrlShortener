using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Handlers.UserHandlers;
using UrlShortener.BLL.Models.UserModels;

namespace Tests.UserRequests
{
    /// <summary>
    /// Тестовый класс для проверки запроса списка пользователей
    /// </summary>
    [TestClass]
    public class GetUsersHandlerTest : UnitTestBase
    {
        private readonly User _user1;
        private readonly User _user2;
        private readonly IAppDbContext _appDbContext;

        public GetUsersHandlerTest()
        {
            _user1 = new("user1", "login1", "foo", UrlShortener.BLL.Constants.RoleType.User);
            _user2 = new("user2", "login2", "foo", UrlShortener.BLL.Constants.RoleType.Admin);

            _appDbContext = CreateInMemoryContext(x => x.AddRange(_user1, _user2));
        }

        /// <summary>
        /// Проверка запроса списка пользователей
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_GetUser_ShouldReturn_GetUserResponse_ShouldFindOne()
        {
            var request = new GetUserQuery()
            {
                Name = "1",
                MinRegistrationDateTime = DateTimeProvider.Object.Now.AddDays(-1),
                MaxRegistrationDateTime = DateTimeProvider.Object.Now.AddDays(1)
            };

            var newHandler = new GetUserHandler(_appDbContext, AuthorizationService.Object);

            var result = await newHandler.Handle(request, default);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            var resultItem = result.First();
            Assert.IsTrue(resultItem.Name.Contains(request.Name));
            Assert.IsTrue(resultItem.RegistrationDateTime >= request.MinRegistrationDateTime);
            Assert.IsTrue(resultItem.RegistrationDateTime <= request.MaxRegistrationDateTime);
        }

        /// <summary>
        /// Проверка запроса списка пользователей с неверными параметрами
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_GetUser_ShouldReturn_GetUserResponse_ShouldFindNone()
        {
            var request = new GetUserQuery()
            {
                Name = "user",
                MinRegistrationDateTime = DateTimeProvider.Object.Now.AddDays(-2),
                MaxRegistrationDateTime = DateTimeProvider.Object.Now.AddDays(-2)
            };

            var newHandler = new GetUserHandler(_appDbContext, AuthorizationService.Object);

            var result = await newHandler.Handle(request, default);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
