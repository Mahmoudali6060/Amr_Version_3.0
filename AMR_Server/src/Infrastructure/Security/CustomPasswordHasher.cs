using Infrastructure.Security;
using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Text;

namespace AMR_Server.Infrastructure.Security
{
    public class CustomPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return Hashing.MD5(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
