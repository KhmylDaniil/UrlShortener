using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Handlers.UrlHandlers;
using UrlShortener.BLL.Models.UrlModels;

namespace Tests.UrlRequests
{
    /// <summary>
    /// Тестовый класс для проверки создания короткого url
    /// </summary>
    [TestClass]
    public class CreateUrlRecordHandlerTest : UnitTestBase
    {
        private readonly IAppDbContext _appDbContext;

        public CreateUrlRecordHandlerTest()
        {
            _appDbContext = CreateInMemoryContext();
        }

        /// <summary>
        /// Метод проверки команды создания записи url
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Handle_CreateUrlRecordHandler_ShouldCreate()
        {
            var request = new CreateUrlRecordCommand()
            {
                LongUrl = "lorem ipsum",
            };

            var handler = new CreateUrlRecordHandler(_appDbContext, UserContext.Object);

            var result = await handler.Handle(request, default);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == 7);

            var record = await _appDbContext.UrlRecords.FirstOrDefaultAsync();
            Assert.IsNotNull(record);

            Assert.AreEqual(request.LongUrl, record.LongUrl);
            Assert.AreEqual(result, record.ShortUrl);
        }
    }
}
