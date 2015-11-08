using DataAccess.Entities;

namespace Services
{
    public interface IUserService
    {
        User Get(int id);
    }
}
