using Azure.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Services;

namespace Tests.Services
{
    /// <summary>
	/// Проверка сервиса авторизации
	/// </summary>
	[TestClass]
    public class AuthorizationServiceTest : UnitTestBase
    {
        /// <summary>
        /// Метод проверки работы метода при корректных данных 
        /// </summary>
        [TestMethod]
        public void AuthorizationCheckTest()
        {
            var authorizationService = new AuthorizationService(UserContext.Object);

            authorizationService.AuthorizationCheck(RoleType.Admin);

            Assert.IsTrue(true);
        }

        /// <summary>
        /// Метод исключения при недостаточных правах 
        /// </summary>
        [TestMethod]
        public void AuthorizationCheckTestShouldThrowException()
        {
            var UserContextAsUser = new Mock<IUserContext>();
            UserContext.Setup(x => x.RoleType).Returns(RoleType.User);

            var authorizationService = new AuthorizationService(UserContextAsUser.Object);

            Assert.ThrowsException<ApplicationSystemBaseException>(
                action: () => authorizationService.AuthorizationCheck(RoleType.Admin),
                message: ExceptionMessages.IncorrectRole);
        }
    }
}
