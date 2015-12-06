using System.Collections.Generic;
using System.Web.Mvc;
using DataAccess.DAO;
using Microsoft.AspNet.Identity;
using PmiOfficial.Models;
using Services;
using Services.DTO;
using Services.ImageServices;
using System.Text;

namespace PmiOfficial.Controllers
{
    public class UserProfileController : Controller
    {
        public IUserService _userService;
        private IImageService _imageService;

        public UserProfileController()
        {
            _userService = new UserService(new UserDAO());
            _imageService = new ImageService();
        }

        // GET: UserProfile
        public ActionResult Index(int userId)
        {
            var user = _userService.Get(userId);
            ViewBag.UserId = userId;
            ViewBag.User = user;
            ViewBag.User.Hobbies = "hobbies";
            ViewBag.User.Plans = new Dictionary<string, List<string>>{ { "Monday", new List<string> { "Rest", "ЧМ" } },
                    { "Tuesday", new List<string> {"Movie" } }, {"Wednesday", new List<string>()}, {"Thursday", new List<string>() }, {"Friday", new List<string>()}};
            
            if(user.ImageId != 0)
            {
                var image = _imageService.Get(user.ImageId);
                if (!string.IsNullOrEmpty(image.PathToLocalImage))
                {
                    ViewBag.UserImage = ConvertLocalServerPathToUrl(image.PathToLocalImage);
                }
            }
            
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
        public ActionResult Edit(EditUserDTO userDto, System.Web.HttpPostedFileBase imageFile)
        {
            userDto.ImageFile = imageFile;
            _userService.Edit(userDto, HttpContext.Server);

            return RedirectToAction("Index", new { userId = userDto.Id });
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

        private string ConvertLocalServerPathToUrl(string localPath)
        {
            var url = new StringBuilder(localPath);

            url.Replace(@"\", @"/");

            int index = localPath.IndexOf(ImageService.LOCAL_FOLDER_TO_SAVE_IMAGES);

            url.Remove(0, index);

            url.Insert(0, @"~/");

            return url.ToString();
        }

    }
}