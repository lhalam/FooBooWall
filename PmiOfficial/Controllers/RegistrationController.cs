using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PmiOfficial.Filters;

namespace PmiOfficial.Controllers
{
    [RequireSecureConnection]
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/
        public ActionResult Index()
        {
            return View();
        }
	}
}