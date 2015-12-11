using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAO;
using DataAccess.Entities;
using DataAccess.Identity;

namespace Services.RetrievalPassword
{
    public class UpdatingPasswordService
    {
        private void UpdateUserPassword(User user, string newPassword)
        {
            CustomUserManager userManager = new CustomUserManager();
            string hashedPassword = userManager.PasswordHasher.HashPassword(newPassword);
            UserDAO userDao = new UserDAO();
            user.PasswordHash = hashedPassword;
            userDao.Update(user);
        }

        private bool CheckIfuserExists(string email)
        {
            UserDAO userDao = new UserDAO();
            User user = userDao.GetUserByEmail(email);
            return user != null;
        }

        public bool SendNotificationLetter(string email)
        {
            if (!CheckIfuserExists(email))
            {
                return false;
            }

            VerificationLetter letter = new VerificationLetter()
            {
                Text = "Code for new password",
                Email = email
            };
            VerificationDAO verDao = new VerificationDAO();
            verDao.Create(letter);
            letter.Send();
            return true;
        }

        public bool CheckCode(string code)
        {
            VerificationDAO verDao = new VerificationDAO();
            VerificationLetter letter = verDao.Read(1);
            bool areSame = (letter.Code == code);
            if (areSame)
            {
                letter.Verified = true;
                verDao.Update(letter);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdatePassword(NewPasswordModel model)
        {
            UserDAO uDao = new UserDAO();
            User user = uDao.GetUserByEmail(model.Email);
            UpdateUserPassword(user, model.Password);
        }
    }
}
