using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Exceptions;

namespace UrlShortener.BLL.Services
{
    /// <summary>
    /// Реализация сервиса хеширования паролей
    /// </summary>
    public sealed class PasswordHasher : IPasswordHasher
    {
        private readonly string _salt;

        public PasswordHasher(string salt) => _salt = salt;

        /// <inheritdoc/>
        public string Hash(string password)
        {
            if (password == null)
                throw new ApplicationSystemNullException<PasswordHasher>(nameof(password));

            byte[] Salt = Encoding.ASCII.GetBytes(_salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        /// <inheritdoc/>
        public bool VerifyHash(string password, string hash)
        {
            if (password == null)
                throw new ApplicationSystemNullException<PasswordHasher>(nameof(password));
            if (hash == null)
                throw new ApplicationSystemNullException<PasswordHasher>(nameof(hash));

            return string.Equals(hash, Hash(password), StringComparison.Ordinal);
        }
    }
}
