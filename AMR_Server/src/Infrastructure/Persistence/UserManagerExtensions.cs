using AMR_Server.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMR_Server.Infrastructure.Persistence
{
    public static class UserManagerExtensions
    {
        public static async Task<IdentityResult> SetMd5PasswordForUser(
            this UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string md5Password)
        {

            user.PasswordHash = HashPassword(userManager, user, md5Password);
            // Roll the security stamp for the user (invalidates security-related tokens)
            await userManager.UpdateSecurityStampAsync(user);
            // Save the changes to the DB and return the result
            return await userManager.UpdateAsync(user);
        }

        public static string HashPassword(UserManager<ApplicationUser> userManager, ApplicationUser user, string md5Password)
        {
            var reHashedPassword = userManager.PasswordHasher.HashPassword(user, md5Password);
            // Replace the format marker so we know to MD5 hash 
            // provided passwords during password verification
            var passwordToStore = ReplaceFormatMarker(reHashedPassword, 0xF0);
            // Replace the old hash with the "updated marker" hash
            return passwordToStore;
        }

        // Replace the fomat marker in Base64 encoded string 
        // Not the most efficient but does the job
        public static string ReplaceFormatMarker(string passwordHash, byte formatMarker)
        {
            var bytes = Convert.FromBase64String(passwordHash);
            bytes[0] = formatMarker;
            return Convert.ToBase64String(bytes);
        }

    }
}
