using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Entities;
using System.Data.SqlClient;
using Services;
using DataAccess.DAO;
using Services.DTO;

namespace PmiOfficial.Controllers
{
    public class UserProfileController : Controller
    {
        public IUserService _userService;

        public UserProfileController()
        {
            _userService = new UserService(new UserDAO());
        }

        // GET: UserProfile
        public ActionResult Index(int userID)
        {
            ViewBag.User = _userService.Get(userID);
            ViewBag.User.Hobbies = "hobbies";
            ViewBag.User.Plans = new Dictionary<string, List<string>>{ { "Monday", new List<string> { "Rest", "ЧМ" } },
                    { "Tuesday", new List<string> {"Movie" } }, {"Wednesday", new List<string>()}, {"Thursday", new List<string>() }, {"Friday", new List<string>()}};
                
            //ViewBag.User = new User
            //{
            //    FirstName = "Padus",
            //    LastName = "Sofija",
            //    Login = "PSofija",
            //    EMail = "sofijaPadus@gmail.com",
            //    Password = "qwertyqwerqwqrqw",
            //    Image_ID = 1,
            //    VK_ID = "vkiD",
            //    FB_ID = "facebook",
            //    Skype = "skype",
            //    Hobbies = "hobbies",
            //    Plans = new Dictionary<string, List<string>>{ { "Monday", new List<string> { "Rest", "ЧМ" } },
            //        { "Tuesday", new List<string> {"Movie" } }, {"Wednesday", new List<string>()}, {"Thursday", new List<string>() }, {"Friday", new List<string>()}
            //    }
            //};

            return View();
        }

        [HttpPost]
        public void Edit(EditUserDTO userDto)
        {
            _userService.Edit(userDto);
        }
    }
}