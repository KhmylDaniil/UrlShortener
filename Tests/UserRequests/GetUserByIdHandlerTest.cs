using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Handlers.UserHandlers;
using UrlShortener.BLL.Models.UserModels;

namespace Tests.UserRequests
{
    /// <summary>
    /// Тестовый класс для проверки запроса пользователя по идентификатору
    /// </summary>
    [TestClass]
    public class GetUserByHandlerTest : UnitTestBase
    {
        private readonly User _user1;
        private readonly IAppDbContext _appDbContext;

        public GetUserByHandlerTest()
        {
            _user1 = new("user1", "login1", "foo", UrlShortener.BLL.Constants.RoleType.User);

            _appDbContext = CreateInMemoryContext(x => x.AddRange(_user1));
        }

        /// <summary>
        /// Проверка запроса ользователя по идентификатору
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_GetUser_ShouldReturn_GetUserByIdResponse()
        {
            var request = new GetUserByIdQuery()
            {
                Id = _user1.Id
            };

            var newHandler = new GetUserByIdHandler(_appDbContext, AuthorizationService.Object);

            var result = await newHandler.Handle(request, default);

            Assert.IsNotNull(result);

            Assert.IsTrue(result.Name == _user1.Name);
            Assert.IsTrue(result.RegistrationDateTime >= _user1.CreatedOn);
            Assert.IsTrue(result.Role == Enum.GetName(_user1.RoleType));
            Assert.IsTrue(result.Login == _user1.Login);
            Assert.IsTrue(result.UrlRecordsCount == _user1.UrlRecordsCount);
        }
    }
}
