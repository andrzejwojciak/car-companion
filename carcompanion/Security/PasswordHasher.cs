using System;
using System.Security.Cryptography;

namespace carcompanion.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedUserPassword(string hashedPassword, string password);

    }

    public class PasswordHasher : IPasswordHasher
    {   
        public string HashPassword(string password)
        { 
            try
            {    
                //TODO: Hashpassword
                return password;
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
                //Todo: Verify hashed password equals to unhashedpassword
                return hashedPassword.Equals(password);
            }
            catch
            {
                return false;
            }

        }
    }
}