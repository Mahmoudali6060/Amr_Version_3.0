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

namespace AMR_Server.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AmrDbContext _context;
        private readonly Settings _settings;
        public IdentityService(UserManager<ApplicationUser> userManager, AmrDbContext context, Settings settings)
        {
            _userManager = userManager;
            _context = context;
            _settings = settings;
        }

        public async Task<string> Register(string username, string password)
        {
            try
            {
                await CreateUserAsync(username, password);
                return "token";
            }
            catch (Exception ex)
            {
                return "Token";
            }
        }

        public async Task<string> Login(string username, string password)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = username,
                Email = username
            };
            var passwordHash = UserManagerExtensions.EncodePasswordToBase64(password);
            var decodedPassword = UserManagerExtensions.DecodeFrom64(passwordHash);

            ///
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == passwordHash);
            if (user != null)
            {
                return GenerateToken(user.Id);
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
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };


            await _userManager.CreateAsync(user, password);
            var result = await _userManager.SetMd5PasswordForUser(user, password);

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
            using (var context = new PrincipalContext(ContextType.Domain, _settings.PrincipalContextName, userName, password))
            {
                if (context.ValidateCredentials(userName, password))
                {

                    UserPrincipal userPrincile = new UserPrincipal(context);
                    PrincipalSearcher searcher = new PrincipalSearcher(userPrincile);
                    UserPrincipal foundUser = searcher.FindAll().Where(x => x.SamAccountName.Equals(userName)).FirstOrDefault() as UserPrincipal;
                    if (foundUser != null)
                    {
                        var user = _userManager.Users.SingleOrDefault(x => x.Id == foundUser.Guid.ToString());

                        if (user == null)
                        {
                            return await GetUserAD(foundUser, password);
                        }
                        else
                        {
                            return GenerateToken(user.Id);
                        }
                    }
                }
            }
            return null;
        }

        private async Task<string> GetUserAD(UserPrincipal foundUser, string password)
        {
            if (foundUser != null)
            {
                var user = _userManager.Users.SingleOrDefault(x => x.UserName == foundUser.SamAccountName && x.PasswordHash == UserManagerExtensions.EncodePasswordToBase64(UserManagerExtensions.EncodePasswordToBase64(password)));
                if (user == null)
                {
                    CreateUserAD(foundUser, UserManagerExtensions.EncodePasswordToBase64(password));
                }
                return GenerateToken(foundUser.Guid.ToString());
            }
            return null;
        }

        private async void CreateUserAD(UserPrincipal foundUser, string _password)
        {
            if (foundUser != null)
            {
                //var user = new User
                //{
                //    ID = foundUser.Guid.Value.ToString(),
                //    UserName = foundUser.SamAccountName
                //};
                await CreateUserAsync(foundUser.SamAccountName, _password);//
            }
        }
        internal string GetPassword(string Password)
        {
            return TripleDES.Encrypt(Password, true);
        }
        public string GenerateToken(string ID)
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

    }
}
