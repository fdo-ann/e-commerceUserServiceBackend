using System.Security.Cryptography;
namespace e_commerceUserServiceBackend.Helpers
{
    public class PasswordHasher
    {
        private static readonly RNGCryptoServiceProvider RNG1 = new RNGCryptoServiceProvider();
        private static readonly RandomNumberGenerator RNG = RandomNumberGenerator.Create();

        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 10000;

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA1,
                HashSize
            );

            byte[] hashBytes = new byte[SaltSize + HashSize];

            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        // this function returns true if the password matches
        public static bool VerifyPassword(string password, string base64Hash)
        {
            byte[] hashBytes;

            try
            {
                hashBytes = Convert.FromBase64String(base64Hash);
            }
            catch (FormatException)
            {
                return false;
            }

            if (hashBytes.Length != SaltSize + HashSize)
                return false;

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            byte[] storedHash = new byte[HashSize];
            Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);

            byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA1,
                HashSize
            );

            return CryptographicOperations.FixedTimeEquals(
                storedHash,
                computedHash
            );
        }

    }
}
