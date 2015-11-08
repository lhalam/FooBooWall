using DataAccess.DAO;
using DataAccess.Entities;

namespace Services
{
    public class UserService : IUserService
    {
        readonly AbstractDAO<User> _dao;

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
