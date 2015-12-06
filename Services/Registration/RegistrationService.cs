using System;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Identity;
using Microsoft.AspNet.Identity;
using DataAccess.DAO;

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
            VerificationDAO vd = new VerificationDAO();
            VerificationLetter vlt = vd.Read(1);
            if(user.EMail != vlt.Email)
            {
                IdentityResult ir = new IdentityResult("Emails are different!");
                return Task.FromResult(ir);
            }
            CustomUserManager userManager = new CustomUserManager();
            return userManager.CreateAsync(user, model.Password);
        }
    }
}
