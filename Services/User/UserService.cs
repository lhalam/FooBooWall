using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAO;
using DataAccess.Entities;

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
    }
}
