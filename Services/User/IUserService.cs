using DataAccess.Entities;
using Services.DTO;

namespace Services
{
    public interface IUserService
    {
        User Get(int id);

        User GetByLoginName(string login);
        void Edit(EditUserDTO user, System.Web.HttpServerUtilityBase serverObj);
    }
}
