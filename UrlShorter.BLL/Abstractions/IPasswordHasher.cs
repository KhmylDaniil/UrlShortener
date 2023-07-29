
namespace UrlShorter.BLL.Abstractions
{
    /// <summary>
    /// Сервис для хеширования пароля
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Хешировать пароль
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Хешированный пароль</returns>
        string Hash(string password);

        /// <summary>
        /// Проверить правильность пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="hash">Хешированный пароль</param>
        /// <returns>Совпадение</returns>
        bool VerifyHash(string password, string hash);
    }
}
