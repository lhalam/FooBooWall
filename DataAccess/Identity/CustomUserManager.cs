using DataAccess.Entities;
using Microsoft.AspNet.Identity;

namespace DataAccess.Identity
{
    public class CustomUserManager : UserManager<User, int>
    {
        public CustomUserManager()
            : base(new CustomUserStore())
        {
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8
            };
            UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = true
            };
        }
    }
}
