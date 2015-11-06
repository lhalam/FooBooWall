using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DataAccess.Entities;

namespace DataAccess.Identity
{
    public class CustomUserManager : UserManager<User, int>
    {
        public CustomUserManager()
            : base(new CustomUserStore())
        {

        }
    }
}
