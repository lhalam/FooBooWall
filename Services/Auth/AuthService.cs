using DataAccess.Entities;
using DataAccess.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthService
    {
        Task<DataAccess.Entities.User> ExternalAuthentication(ExternalLoginData externalLogin, UserLoginInfo info);
    }

    public class AuthService : IAuthService
    {
        CustomUserManager userManager = new CustomUserManager();
        public async Task<DataAccess.Entities.User> ExternalAuthentication(ExternalLoginData externalLogin, UserLoginInfo info)
        {
            User user = await userManager.FindAsync(info);

            bool hasRegistered = user != null;

            if (!hasRegistered)
            {
                user = new User
                {
                    Login = externalLogin.UserName,
                    FirstName = externalLogin.FirstName,
                    LastName = externalLogin.LastName,
                    Birthday = DateTime.Now
                };
                IdentityResult result =
                    await userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception();
                }

                await userManager.AddLoginAsync(user.Id, info);
            }
            return user;
        }

    }
}
