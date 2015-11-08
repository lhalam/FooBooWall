using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmiOfficial.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index() 
        { 
            Event dummyEvent = new Event();

            dummyEvent.Decription = "Description";
            dummyEvent.Id = 0;
            dummyEvent.ImageId = 1001;
            dummyEvent.Location = "Location";
            dummyEvent.Name = "EventName";
            dummyEvent.OrganizerId = 1;
            dummyEvent.Time = DateTime.Now;

            ViewBag.Event = dummyEvent;

            return View();
        }
    }
}