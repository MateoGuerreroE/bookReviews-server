using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BookReviewsApp.Data.Helpers
{
    public class PasswordHasher
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            string inputHash = HashPassword(password, salt);
            return inputHash == hashedPassword;
        }
    }
}
