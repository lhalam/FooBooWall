using PmiOfficial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.DAO;
using Services.Registration;

namespace Services
{
    public class RegistrationService
    {
        private static UserDAO userDAO = new UserDAO();
        public const string SUCH_LOGIN_ALREADY_EXISTS_ERROR = "such login already exists";
        public static RegistrationResult Register(RegistrationBindingModel model)
        {
            RegistrationResult result = new RegistrationResult();
            if (!userDAO.UserWithSpecifiedLoginExists(model.Login))
            {
                ImageDAO imageDAO = new ImageDAO();
                imageDAO.Create(new Image() { Name = "imageName1", ID = 1 });
                User user = new User();
                user.Login = model.Login;
                user.Password = model.Password;
                user.EMail = model.Email;
                user.FirstName = "Johny";
                user.FB_ID = "-1";
                user.VK_ID = "0";
                user.Image_ID = 1;
                user.Birthday = DateTime.Now;
                userDAO.Create(user);
                result.Succeded = true;
            }
            else
            {
                result.Errors.Add(SUCH_LOGIN_ALREADY_EXISTS_ERROR);
                result.Succeded = false;
            }
            return result;
        }
    }
}
