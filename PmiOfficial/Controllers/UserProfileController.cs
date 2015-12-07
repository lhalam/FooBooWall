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
using System.Linq;

namespace PmiOfficial.Controllers
{
    public class UserProfileController : Controller
    {
        public IUserService UserService;

        public UserProfileController()
        {
            UserService = new UserService(new UserDAO());
        }

        // GET: UserProfile
        public ActionResult Index(int userId)
        {
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

            int index = localPath.IndexOf(ImageService.LOCAL_FOLDER_TO_SAVE_IMAGES);

            url.Remove(0, index);

            url.Insert(0, @"~/");

            return url.ToString();
        }
    }
}