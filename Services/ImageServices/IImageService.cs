using DataAccess.Entities;
using Services.DTO;

namespace Services.ImageServices
{
    public interface IImageService
    {
        Image Get(int id);

        Image Edit(int id);

        /// <summary>
        /// Stores image in local hardware
        /// and saves path to the image in Image object
        /// </summary>
        /// <param name="imageFile">Image file that is received from client</param>
        /// <param name="serverObj">Server instance</param>
        /// <returns></returns>
        Image Create(System.Web.HttpPostedFileBase imageFile, System.Web.HttpServerUtilityBase serverObj);
    }
}
