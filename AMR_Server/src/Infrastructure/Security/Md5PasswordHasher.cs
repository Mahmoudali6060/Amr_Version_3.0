using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AMR_Server.Infrastructure.Security
{
    public class Md5PasswordHasher<TUser> : PasswordHasher<TUser> where TUser : class
    {
        public override PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // read the format marker from the hashed password
            if (decodedHashedPassword.Length == 0)
            {
                return PasswordVerificationResult.Failed;
            }

            // ASP.NET Core uses 0x00 and 0x01 for v2 and v3
            if (decodedHashedPassword[0] == 0xF0)
            {
                // replace the 0xF0 prefix in the stored password with 0x01 (ASP.NET Core Identity V3) and convert back to Base64
                decodedHashedPassword[0] = 0x01;
                var storedPassword = Convert.ToBase64String(decodedHashedPassword);

                // md5 hash the provided password
                var md5ProvidedPassword = GetM5Hash(providedPassword);

                // call the base implementation with the new values
                var result = base.VerifyHashedPassword(user, storedPassword, md5ProvidedPassword);

                return result == PasswordVerificationResult.Success
                    ? PasswordVerificationResult.SuccessRehashNeeded
                    : result;
            }

            return base.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }

        public static string GetM5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                return Convert.ToBase64String(bytes);
            }
        }
    }
}
