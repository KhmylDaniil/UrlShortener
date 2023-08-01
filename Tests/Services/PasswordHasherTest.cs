using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Services;

namespace Tests.Services
{
    [TestClass]
    public class PasswordHasherTest : UnitTestBase
    {
        /// <summary>
		/// Проверка работы алгоритма пароля
		/// </summary>
		/// <param name="salt">Соль</param>
		/// <param name="password">Пароль</param>
		/// <param name="expectedPassword">Ожидаемый пароль</param>
		/// <returns></returns>
		[DataRow("1", "2", "pMfNI9obZJmKEi8loD0tFxZcYFu4hoNyLwhmMSGKy3I=")]
        [DataRow("2", "2", "cTlVOEINQZa1IqglSZUxNzVMtGNjeqiPNa11x35gKY4=")]
        [TestMethod]
        public void Hash_ByPasswordHasher_ShouldFindHashedPassword
            (string salt, string password, string expectedPassword)
        {
            var passwordHasher = new PasswordHasher(salt);

            var result = passwordHasher.Hash(password);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedPassword, result);
        }

        /// <summary>
        /// Проверка исключения при вводе пустого пароля для хеширования
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Hash_ByPasswordHasher_ShouldThrowArgumentNullException()
        {
            var passwordHasher = new PasswordHasher("a");

            Assert.ThrowsException<ApplicationSystemNullException<PasswordHasher>>(() =>
                passwordHasher.Hash(null));
        }

        /// <summary>
        /// Проверка работы верификации пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="hash">Хеш</param>
        /// <param name="expectedBool">Ожидаемый логический результат</param>
        /// <returns></returns>
        [DataRow("2", "pMfNI9obZJmKEi8loD0tFxZcYFu4hoNyLwhmMSGKy3I=", true)]
        [DataRow("1", "1", false)]
        [TestMethod]
        public void VerifyHash_ByPasswordHasher_ShouldFindBoole
            (string password, string hash, bool expectedBool)
        {
            var passwordHasher = new PasswordHasher("1");

            var result = passwordHasher.VerifyHash(password, hash);

            Assert.AreEqual(expectedBool, result);
        }

        /// <summary>
        /// Проверка исключения при вводе пустого пароля для проверки
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="hash">Ожидаемый пароль</param>
        /// <returns></returns>
        [DataRow(null, "1")]
        [DataRow("1", null)]
        [TestMethod]
        public void VerifyHash_ByPasswordHasher_ShouldThrowArgumentNullException
            (string password, string hash)
        {
            var passwordHasher = new PasswordHasher("1");

            Assert.ThrowsException<ApplicationSystemNullException<PasswordHasher>>(() =>
                passwordHasher.VerifyHash(password, hash));
        }
    }
}
