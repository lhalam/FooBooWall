﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace PmiOfficial.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register()
        {
            string user = Request["txtLogin"];
            StringBuilder sbInterest = new StringBuilder();
            sbInterest.Append("<b> УРААААА!!!!!! ЮЗЕР " + user + " ЗАРЕЄСТРОВАНИЙ!!! <br/>");
            return Content(sbInterest.ToString());
        }
    }
}