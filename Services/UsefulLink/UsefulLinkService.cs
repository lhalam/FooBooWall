using DataAccess.DAO;
using DataAccess.Entities;
using Services.DTO;
using Services.ImageServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Services
{
    public class UsefulLinkService : IUsefulLinkService
    {
        private readonly AbstractDAO<UsefulLink> _dao;

        private readonly IImageService _imageService;

        public static string DEFAULT_USEFUL_LINK_IMAGE_URL = "http://www.joomlack.fr/images/demos/demo2/on-top-of-earth.jpg";

        public UsefulLinkService(AbstractDAO<UsefulLink> dao)
        {
            _dao = dao;
            _imageService = new ImageService();
        }

        private bool IsValidUrl(string URL)
        {
            try
            {
                var req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool IsValidImageUrl(string URL)
        {
            try
            {
                var req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                req.Method = "HEAD";
                using (var resp = req.GetResponse())
                {
                    return resp.ContentType.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                               .StartsWith("image/");
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public UsefulLinkResult Create(UsefulLinkDTO entity)
        {
            UsefulLinkResult res = new UsefulLinkResult();
            string img_url;
            if (IsValidImageUrl(entity.ImageUrl))
            {
                img_url = entity.ImageUrl;
            }
            else
	        {
                img_url = DEFAULT_USEFUL_LINK_IMAGE_URL;
	        }

            if (!string.IsNullOrEmpty(entity.Url) && IsValidUrl(entity.Url))
            {
                if (!string.IsNullOrEmpty(entity.Name))
                {
                    UsefulLink usefulLink = new UsefulLink
                    {
                        Comment = entity.Comment,
                        ImageUrl = img_url,
                        Name = entity.Name,
                        OwnerUserID = entity.OwnerUserID,
                        Url = entity.Url
                    };
                    _dao.Create(usefulLink);
                    res.Succeeded = true;
                }
                else
                {
                    res.Succeeded = false;
                    res.Errors.Add("error occured : useful link should have a name");
                }
            }
            else
            {
                res.Succeeded = false;
                res.Errors.Add("error occured : useful link should have a valid url");
            }
            return res;
        }

        public UsefulLink Get(int id)
        {
            return _dao.Read(id);
        }

        public void Delete(UsefulLinkDTO entity)
        {
            UsefulLink usefulLink = new UsefulLink
            {
                Comment = entity.Comment,
                Id = entity.Id,
                ImageUrl = entity.ImageUrl,
                Name = entity.Name,
                OwnerUserID = entity.OwnerUserID,
                Url = entity.Url
            };
            _dao.Delete(usefulLink);
        }

        public void Edit(EditUsefulLinkDTO usefulLinkDto)
        {
            //does nothing yet!


            //UsefulLink u = Get(usefulLinkDto.Id);
            

            //u.FirstName = usefulLinkDto.FirstName;
            //u.LastName = usefulLinkDto.LastName;
            //u.EMail = usefulLinkDto.Email;
            //u.Skype = usefulLinkDto.SkypeName;

            //long ticks = new DateTime(1970, 1, 2).Ticks;
            //DateTime dt = new DateTime(ticks);

            //u.Birthday = dt.AddMilliseconds(usefulLinkDto.Birthday);



            //if (usefulLinkDto.ImageFile != null)
            //{
            //    var image = _imageService.Create(usefulLinkDto.ImageFile, serverObj);
            //    u.ImageId = image.Id;
            //}

            //_dao.Update(u);
        }

        public List<UsefulLink> ReadAll()
        {
            return _dao.ReadAll();
        }
    }
}
