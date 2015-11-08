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
    public class UserService : IUserService
    {
        AbstractDAO<User> _dao;

        public UserService(AbstractDAO<User> dao)
        {
            _dao = dao;
        }

        public User Get(int id)
        {
            return _dao.Read(id);
        }

        public void Edit(EditUserDTO userDto)
        {
            User u = Get(userDto.Id);
            u.FirstName = userDto.FirstName;
            u.LastName = userDto.LastName;
            u.EMail = userDto.Email;
            u.Birthday = DateTime.Parse(userDto.Birthday);
            _dao.Update(u);
        }
    }
}
