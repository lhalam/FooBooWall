using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PmiOfficial.Filters;
using PmiOfficial.Models;

namespace PmiOfficial.Controllers
{
    [RequireSecureConnection]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

    }
}