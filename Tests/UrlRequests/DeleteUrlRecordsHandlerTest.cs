using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Handlers.UrlHandlers;
using UrlShortener.BLL.Handlers.UserHandlers;
using UrlShortener.BLL.Models.UrlModels;
using UrlShortener.BLL.Models.UserModels;

namespace Tests.UrlRequests
{
    /// <summary>
    /// Тестовый класс для проверки удаления записей из базы
    /// </summary>
    [TestClass]
    public class DeleteUrlRecordsHandlerTest : UnitTestBase
    {
        private readonly User _user;
        private readonly UrlRecord _urlRecord1;
        private readonly UrlRecord _urlRecord2;
        private readonly IAppDbContext _appDbContext;

        public DeleteUrlRecordsHandlerTest()
        {
            _user = new("user1", "login1", "foo", UrlShortener.BLL.Constants.RoleType.User);

            _urlRecord1 = new("qwertyu", "lorem ipsum", _user);
            _urlRecord2 = new("asdfghj", "ipsum lorem", null);

            _appDbContext = CreateInMemoryContext(x => x.AddRange(_user, _urlRecord1, _urlRecord2));
        }

        /// <summary>
        /// Проверка запроса списка пользователей
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_DeleteUrlRecords_ShouldDeleteOneRecord()
        {
            var request = new DeleteUrlRecordsCommand()
            {
               UserId = _user.Id
            };

            var newHandler = new DeleteUrlRecordsHandler(_appDbContext, AuthorizationService.Object, DateTimeProvider.Object);

            var result = await newHandler.Handle(request, default);

            Assert.IsNotNull(result);

            var records = _appDbContext.UrlRecords.ToList();

            Assert.AreEqual(1, records.Count);
            Assert.AreEqual(_urlRecord2.Id, records[0].Id);
        }

        /// <summary>
        /// Проверка удаления по давности создания
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_DeleteUrlRecords_ShouldDeleteBothRecords()
        {
            var request = new DeleteUrlRecordsCommand()
            {
                Days = 1,
            };

            var newHandler = new DeleteUrlRecordsHandler(_appDbContext, AuthorizationService.Object, DateTimeProvider.Object);

            var result = await newHandler.Handle(request, default);

            Assert.IsNotNull(result);

            var records = _appDbContext.UrlRecords.ToList();

            Assert.AreEqual(0, records.Count);
        }
    }
}
