using DataAccess.Entities;
using DataAccess.DAO;
using Services.DTO;

namespace Services
{
    public class EventService: IEventService
    {
        private EventDAO _dao;

        public EventService(EventDAO dao) 
        {
            _dao = dao;        
        }

        public Event Get(int id)
        {
            return _dao.Read(id);
        }

        public void Edit(EventDTO eventDTO)
        {
            Event e = Get(eventDTO.Id);
            e.Decription = eventDTO.Decription;
            e.ImageId = eventDTO.ImageId;
            e.Location = eventDTO.Location;
            e.Name = eventDTO.Name;
            e.Time = eventDTO.Time;

            _dao.Update(e);
        }

        public void Delete(int id)
        {
            Event e = Get(id);

            _dao.Delete(e);
        }

        public void Create(EventDTO eventDTO, int userId)
        {
            Event e = new Event();

            e.Id = eventDTO.Id;
            e.ImageId = eventDTO.ImageId;
            e.Location = eventDTO.Location;
            e.Name = eventDTO.Name;
            e.OrganizerId = userId;
            e.Time = eventDTO.Time;

            _dao.Create(e);
        }
    }
}
