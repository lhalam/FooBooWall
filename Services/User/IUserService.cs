using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAO;
using DataAccess.Entities;
using Services.DTO;

namespace Services
{
    public interface IUserService
    {
        User Get(int id);

        void Edit(EditUserDTO user);
    }
}
