using DataAccess.Entities;
using Services.DTO;
using System.Collections.Generic;

namespace Services
{
    public interface IEventService
    {
        Event Get(int id);

        void Edit(EventDTO eventDTO);
        
        void Delete(int id);

        void Create(EventDTO eventDTO, int userId);
        IEnumerable<Event> Get();
    }
}
