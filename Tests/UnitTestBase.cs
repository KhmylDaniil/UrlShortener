using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Services;
using UrlShortener.DAL;

namespace Tests
{
    /// <summary>
	/// Базовый класс для модульных тестов
	/// </summary>
	public class UnitTestBase : IDisposable
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        protected Guid UserId = new("8094e0d0-3148-4791-9053-9667cbe137d8");

        /// <summary>
        /// Мок контекста текущего пользователя как админа
        /// </summary>
        protected Mock<IUserContext> UserContext { get; private set; }

        /// <summary>
        /// Мок сервиса проверки прав доступа
        /// </summary>
        protected Mock<IAuthorizationService> AuthorizationService { get; private set; }

        /// <summary>
        /// Мок сервиса хеширования пароля
        /// </summary>
        protected Mock<IPasswordHasher> PasswordHasher { get; private set; }

        /// <summary>
        /// Провайдер даты и времени
        /// </summary>
        protected Mock<IDateTimeProvider> DateTimeProvider { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public UnitTestBase()
        {
            UserContext = new Mock<IUserContext>();
            UserContext.Setup(x => x.CurrentUserId).Returns(UserId);
            UserContext.Setup(x => x.RoleType).Returns(RoleType.Admin);

            AuthorizationService = new Mock<IAuthorizationService>();
            AuthorizationService.Setup(x => x.AuthorizationCheck(It.IsAny<RoleType>()));

            PasswordHasher = new Mock<IPasswordHasher>();
            PasswordHasher.Setup(x => x.Hash(It.IsAny<string>())).Returns("foo");
            PasswordHasher.Setup(x => x.VerifyHash(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            DateTimeProvider = new Mock<IDateTimeProvider>();
            DateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2022, 1, 10));
        }

        /// <summary>
        /// Создать контекст с БД в памяти
        /// </summary>
        /// <returns>Контекст AppDbContext</returns>
        protected AppDbContext CreateInMemoryContext(Action<AppDbContext> dbSeeder = null)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"Test{Guid.NewGuid()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new AppDbContext(options, UserContext.Object, DateTimeProvider.Object))
            {
                dbSeeder?.Invoke(context);
                context.SaveChangesAsync().GetAwaiter().GetResult();
            }
            return new AppDbContext(options, UserContext.Object, DateTimeProvider.Object);
        }

        /// <inheritdoc/>
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
