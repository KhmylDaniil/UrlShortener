using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Handlers.UrlHandlers;
using UrlShortener.BLL.Models.UrlModels;

namespace Tests.UrlRequests
{
    /// <summary>
    /// Тестовый класс для проверки получения длинного url из базы
    /// </summary>
    [TestClass]
    public class GetLongUrlHandlerTest : UnitTestBase
    {
        private readonly UrlRecord _urlRecord;
        private readonly IAppDbContext _appDbContext;

        public GetLongUrlHandlerTest()
        {
            _urlRecord = new("qwertyu", "lorem ipsum", null);

            _appDbContext = CreateInMemoryContext(x => x.AddRange(_urlRecord));
        }

        /// <summary>
        /// Метод проверки получения длинного url из базы
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_GetLongUrlHandler_ShouldGetLongUrl()
        {
            var request = new GetLongUrlQuery(_urlRecord.ShortUrl);

            var handler = new GetLongUrlHandler(_appDbContext);

            var result = await handler.Handle(request, default);

            Assert.IsNotNull(result);
            Assert.IsTrue(result == _urlRecord.LongUrl);

        }
    }
}
