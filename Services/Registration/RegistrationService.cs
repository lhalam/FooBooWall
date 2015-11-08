using System;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Identity;
using Microsoft.AspNet.Identity;

namespace Services.Registration
{
    public class RegistrationService
    {
        public static Task<IdentityResult> Register(RegistrationBindingModel model)
        {
            User user = new User
            {
                Login = model.Login,
                EMail = model.Email,
                ImageId = 1,
                Birthday = DateTime.Now
            };
            CustomUserManager userManager = new CustomUserManager();
            return userManager.CreateAsync(user, model.Password);
        }
    }
}
