using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace carcompanion.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedUserPassword(string hashedPassword, string password);

    }

    public class PasswordHasher : IPasswordHasher
    {
        private readonly byte[] salt = new byte[128 / 8];

        public string HashPassword(string password)
        {
            try
            {
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                return hashedPassword;
            }
            catch
            {
                return null;
            }                
        }

        public bool VerifyHashedUserPassword(string hashedPassword, string password)
        {
            try
            {          
                if (hashedPassword == HashPassword(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}