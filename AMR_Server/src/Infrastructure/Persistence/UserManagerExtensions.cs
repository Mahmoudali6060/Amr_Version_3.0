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
        public static async Task<IdentityResult> SetPasswordHashForUser(
            this UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string md5Password)
        {

            //user.PasswordHash = HashPassword(userManager, user, md5Password);
            user.PasswordHash = EncodePasswordToBase64(md5Password);

            // Roll the security stamp for the user (invalidates security-related tokens)
            await userManager.UpdateSecurityStampAsync(user);
            // Save the changes to the DB and return the result
            var result = await userManager.UpdateAsync(user);
            return result;
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

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

    }
}
