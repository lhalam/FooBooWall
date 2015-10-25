using PmiOfficial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.DAO;

namespace Services
{
    public class RegistrationService
    {
        public static UserDAO userDAO;
        public static RegistrationResult Register(RegistrationBindingModel model)
        {
            User user = new User();
            user.Login = model.Login;
            user.Password = model.Password;
            user.EMail = model.Email;
            
        }
    }
}
