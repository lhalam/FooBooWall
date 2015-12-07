using PmiOfficial.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PmiOfficial.Controllers
{
    public class EventApiController : ApiController
    {
        IEventService _service = new EventService(new DataAccess.DAO.EventDAO());

        public IEnumerable<EventBindingModel> Get()
        {
            return _service.Get().Select(t => new EventBindingModel(t.Name, t.Time, t.Id)).ToList() ;
        }
    }
}
