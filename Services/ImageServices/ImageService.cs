using DataAccess.DAO;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageServices
{
    public class ImageService : IImageService
    {
        public const string LOCAL_FOLDER_TO_SAVE_IMAGES = "UsersImages";

        private static readonly string[] allowedExtensions = new string[]
        {
            ".jpeg",
            ".png",
            ".jpg"
        };

        private AbstractDAO<Image> imageDAO;

        public ImageService()
        {
            imageDAO = new ImageDAO();
        }

        public DataAccess.Entities.Image Get(int id)
        {
            return imageDAO.Read(id);
        }

        public DataAccess.Entities.Image Edit(int id)
        {
            throw new NotImplementedException();
        }


        public Image Create(System.Web.HttpPostedFileBase imageFile, System.Web.HttpServerUtilityBase serverObj)
        {
            var daoImage = new Image();

            var fileName = Path.GetFileName(imageFile.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext = Path.GetExtension(imageFile.FileName); //getting the extension(ex-.jpg)  
            if (allowedExtensions.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                string myfile = name + "_" + ext; //appending the name with id  
                // store the file inside ~/project folder(Img)  
                var path = Path.Combine(serverObj.MapPath("~/" + LOCAL_FOLDER_TO_SAVE_IMAGES), myfile);
                daoImage.PathToLocalImage = path;

                imageFile.SaveAs(path);

                imageDAO.Create(daoImage);
            }
            else
            {
                throw new ArgumentException("Image format is not supported");
            }

            return daoImage;
        }
    }
}
