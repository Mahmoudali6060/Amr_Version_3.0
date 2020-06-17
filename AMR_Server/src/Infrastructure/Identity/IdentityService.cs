using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using Infrastructure.Security;
using AMR_Server.Infrastructure.Persistence;
using AMR_Server.Domain.Entities;
using AMR_Server.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace AMR_Server.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;//To access AspNetUsers table (Identity table)  
        private readonly AmrDbContext _context;//to Access other tables in context such as UserBasicData
        private readonly IOptions<Settings> _settings;//Getting of values from appsettings.json file 
        public IdentityService(UserManager<ApplicationUser> userManager, AmrDbContext context, IOptions<Settings> settings)
        {
            _userManager = userManager;
            _context = context;
            _settings = settings;
        }

        #region Main Methods (Implemented Methods)
        public async Task<string> Register(string username, string password)
        {
            var result = await CreateUserAsync(username, password);//Insert row in AspNetUsers (Identity)
            if (result.Result.Succeeded)
            {
                await CreateUserBasicDataAsync(result.UserId, username);//Insert row in UserBasicData table (Details data about user)
                return GenerateToken(result.UserId);//Generate Token if Usser has been created
            }
            return null;
        }
        public async Task<string> Login(string username, string password)
        {
            var passwordHash = UserManagerExtensions.EncodePasswordToBase64(password);//Encoding of passed Password and compare with PasswordHash in AspNetUsers table
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == passwordHash);
            if (user != null)
            {
                return GenerateToken(user.Id);//Generate token If Username and Password is valid
            }
            return null;
        }
        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }
        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            //Prepare AspNetUsers record
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            await _userManager.CreateAsync(user, password);
            var result = await _userManager.SetPasswordHashForUser(user, password);//Hashing of Password
            return (result.ToApplicationResult(), user.Id);
        }
        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return await DeleteUserAsync(user);
            }
            return Result.Success();

        }
        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.ToApplicationResult();
        }
        public async Task<string> AuthenticateAD(string userName, string password)
        {
            //Check if Username and Password in Domain or no (Active Directory)
            using (var context = new PrincipalContext(ContextType.Domain, _settings.Value.PrincipalContextName, userName, password))
            {
                if (context.ValidateCredentials(userName, password))
                {
                    UserPrincipal userPrincile = new UserPrincipal(context);
                    PrincipalSearcher searcher = new PrincipalSearcher(userPrincile);
                    UserPrincipal user = searcher.FindAll().Where(x => x.SamAccountName.Equals(userName)).FirstOrDefault() as UserPrincipal;
                    return await GetUserAD(user, password);
                }
            }
            return null;
        }
        #endregion

        #region Helper Methods
        private async Task<int> CreateUserBasicDataAsync(string aspNetUserId, string username)
        {
            UserBasicData user = new UserBasicData()
            {
                Aspnetuserid = aspNetUserId,
                UserName = username
            };
            await _context.UserBasicData.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }
        private async Task<string> GetUserAD(UserPrincipal foundUser, string password)
        {
            if (foundUser != null)
            {
                var user = _userManager.Users.SingleOrDefault(x => x.UserName == foundUser.SamAccountName);
                if (user == null)
                {
                    password = "StingRay@123";//Complex Password for Active Directory >>>[TO-DO] It is so bad solution now,So you can solve this problem later
                    var result = await CreateUserAsync(foundUser.SamAccountName, password);
                    int userId = await CreateUserBasicDataAsync(result.UserId, foundUser.SamAccountName); //Insert row in UserBasicData table
                }
                return GenerateToken(foundUser.Guid.ToString());
            }
            return null;
        }
        private string GenerateToken(string ID)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes("F648Ic/rdhJghjkDYxgY9vj/ENI=");
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, ID.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
        #endregion

    }
}
