using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmiOfficial.Controllers
{
    public class EventCalendarController : Controller
    {
        // GET: EventCalendar
        public ActionResult Index()
        {
            return View();
        }
    }
}