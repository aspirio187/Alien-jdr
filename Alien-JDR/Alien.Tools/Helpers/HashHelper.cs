using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alien.Tools.Helpers
{
    public static class HashHelper
    {
        public static string HashUsingPbkdf2(string password, string salt)
        {
            using Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(Hash(salt)), 10000, HashAlgorithmName.SHA256);
            byte[] derivedRandomKey = bytes.GetBytes(32);
            string hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }

        private static string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
