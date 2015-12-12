using DataAccess.Entities;
using Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Services
{
    public interface IUsefulLinkService
    {
        UsefulLinkResult Create(UsefulLinkDTO entity);
        void Delete(UsefulLinkDTO entity);
        UsefulLink Get(int id);
        void Edit(EditUsefulLinkDTO entity);
        List<UsefulLink> ReadAll();
    }
}
