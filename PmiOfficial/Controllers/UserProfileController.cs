using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataAccess.DAO;
using Microsoft.AspNet.Identity;
using PmiOfficial.Models;
using Services;
using Services.DTO;
using Services.ImageServices;
using DataAccess.Entities;
<<<<<<< HEAD
using System.Linq;
=======
>>>>>>> df600a60b8f3b3e34b025c42f19c9381dc8dd3f5

namespace PmiOfficial.Controllers
{
    public class UserProfileController : Controller
    {
        public IUserService UserService;
<<<<<<< HEAD
=======
        public IImageService ImagesService;
>>>>>>> df600a60b8f3b3e34b025c42f19c9381dc8dd3f5

        public UserProfileController()
        {
            UserService = new UserService(new UserDAO());
<<<<<<< HEAD
=======
            ImagesService = new ImageService();
>>>>>>> df600a60b8f3b3e34b025c42f19c9381dc8dd3f5
        }

        // GET: UserProfile
        public ActionResult Index(int userId)
        {
<<<<<<< HEAD
            UsefulLinkDAO usefulLinkDAO = new UsefulLinkDAO();
            ImageDAO imageDAO = new ImageDAO();

            User user = UserService.Get(userId);
            ViewBag.User = user;
            List<UsefulLink> list = usefulLinkDAO.ReadAll();
            if (list.Count == 0)
            {
                ViewBag.UsefulLinks = new List<UsefulLinkDTO>();
            }
            else
            {
                ViewBag.UsefulLinks = from x in list
                                           where x.OwnerUserID == user.Id
                                           select new UsefulLinkDTO
                                           {
                                               Id = x.Id,
                                               Comment = x.Comment,
                                               ImageUrl = imageDAO.Read(x.ImageId ?? 0).Name,
                                               Name = x.Name,
                                               OwnerUserID = x.OwnerUserID,
                                               Url = x.Url
                                           };
            }
            
=======
            User user = UserService.Get(userId);
            ViewBag.User = user;
            ViewBag.AvatarPath = ImagesService.Get(user.ImageId).Name;
>>>>>>> df600a60b8f3b3e34b025c42f19c9381dc8dd3f5
            ViewBag.User.Hobbies = "hobbies";
            ViewBag.User.Plans = new Dictionary<string, List<string>>{ { "Monday", new List<string> { "Rest", "ЧМ" } },
                    { "Tuesday", new List<string> {"Movie" } }, {"Wednesday", new List<string>()}, {"Thursday", new List<string>() }, {"Friday", new List<string>()}};

            return View();
        }

        [HttpPost]
        public void Edit(EditUserDTO userDto)
        {
            UserService.Edit(userDto, new HttpServerUtilityWrapper(System.Web.HttpContext.Current.Server));
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
            //перед тим йшл
            int index = localPath.IndexOf(ImageService.LOCAL_FOLDER_TO_SAVE_IMAGES);

            url.Remove(0, index);

            url.Insert(0, @"~/");

            return url.ToString();
        }
    }
}