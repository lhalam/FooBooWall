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
