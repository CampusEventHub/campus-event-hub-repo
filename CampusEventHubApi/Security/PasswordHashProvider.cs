using System.Security.Cryptography;
using System.Text;

namespace CampusEventHubApi.Security
{
    public static class PasswordHashProvider
    {
        public static string GetSalt()
        {
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string GetHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var hmac = new HMACSHA512(saltBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
        {
            var computedHash = GetHash(inputPassword, storedSalt);
            return storedHash == computedHash;
        }
    }
}
