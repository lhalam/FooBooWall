using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Entities;

namespace PmiOfficial.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            ViewBag.User = new User
            {
                FirstName = "Padus",
                LastName = "Sofija",
                EMail = "sofijaPadus@gmail.com",
                VK_ID = "vkiD",
                FB_ID = "facebook",
                Skype = "skype",
                Hobbies = "hobbies",
                Plans = new Dictionary<string, List<string>>{ { "Monday", new List<string> { "Rest", "ЧМ" } },
                    { "Tuesday", new List<string> {"Movie" } }, {"Wednessday", new List<string>()}, {"Thursday", new List<string>() }, {"Friday", new List<string>()}
                }
            };
            return View();
        }
    }
}