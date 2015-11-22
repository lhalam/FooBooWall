using System.Collections.Generic;
using System.Web.Mvc;
using DataAccess.DAO;
using Microsoft.AspNet.Identity;
using PmiOfficial.Models;
using Services;
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
        public ActionResult Index(int userId)
        {
            ViewBag.User = _userService.Get(userId);
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


        public PartialViewResult UserInfo()
        {
            UserInfo info = new UserInfo
            {
                Id = User.Identity.GetUserId<int>(),
                Name = User.Identity.Name,
                IsAuthenticated = User.Identity.IsAuthenticated
            };
            return PartialView("UserInfo", info);
        }

    }
}