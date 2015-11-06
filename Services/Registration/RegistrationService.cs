using PmiOfficial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.DAO;
using Services.Registration;
using DataAccess.Identity;
using Microsoft.AspNet.Identity;

namespace Services
{
    public class RegistrationService
    {
        public static Task<IdentityResult> Register(RegistrationBindingModel model)
        {
            User user = new User
            {
                Login = model.Login,
                EMail = model.Email,
                Image_ID = 1,
                Birthday = DateTime.Now
            };
            CustomUserManager userManager = new CustomUserManager();
            return userManager.CreateAsync(user, model.Password);
        }
    }
}
