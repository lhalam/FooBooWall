using DataAccess.DAO;
using DataAccess.Entities;
using Services.DTO;
using System;

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

        public void Edit(EditUserDTO userDto)
        {
            User u = Get(userDto.Id);
            u.FirstName = userDto.FirstName;
            u.LastName = userDto.LastName;
            u.EMail = userDto.Email;
            u.Skype = userDto.SkypeName;

            long ticks = new DateTime(1970, 1, 2).Ticks;
            DateTime dt = new DateTime(ticks);

            u.Birthday = dt.AddMilliseconds(userDto.Birthday);
            _dao.Update(u);
        }
    }
}
