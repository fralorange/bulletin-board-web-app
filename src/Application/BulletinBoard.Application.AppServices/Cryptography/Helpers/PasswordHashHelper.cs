using System.Security.Cryptography;

namespace BulletinBoard.Application.AppServices.Cryptography.Helpers
{
    /// <summary>
    /// Хелпер для хеширования паролей.
    /// </summary>
    public static class PasswordHashHelper
    {
        private const int SaltSize = 16;
        private const int SaltHashSize = 32;
        private const int Iterations = 50000;

        private static string GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        /// <summary>
        /// Хеширование пароля с использованием соли.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Хешированный пароль.</returns>
        public static (string Salt, string Hash) HashPassword(string password)
        {
            var salt = GenerateSalt();
            byte[] saltBytes = Convert.FromBase64String(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256)) 
            {
                byte[] hash = pbkdf2.GetBytes(SaltHashSize);
                return (salt, Convert.ToBase64String(hash));
            }
        }

        /// <summary>
        /// Хеширование пароля с использованием уже зараннее сгенерированной соли.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(SaltHashSize);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
