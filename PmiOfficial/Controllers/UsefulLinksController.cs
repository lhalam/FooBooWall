using DataAccess.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.DAO;

namespace PmiOfficial.Controllers
{
    public class UsefulLinksController : ApiController
    {
        UsefulLinkDAO usefulLinkDAO = new UsefulLinkDAO();
        ImageDAO imageDAO = new ImageDAO();
        
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void AddUsefulLink([FromBody]UsefulLinkDTO entity)
        {
            //Image newImg = new Image { Name = "UsefulLink", PathToLocalImage = entity.ImageUrl };
            //imageDAO.Create(newImg);
            UsefulLink usefulLink = new UsefulLink
            {
                Comment = entity.Comment,
                Id = entity.Id,
                ImageId = 1/*newImg.Id*/,
                Name = entity.Name,
                OwnerUserID = entity.OwnerUserID,
                Url = entity.Url
            };
            usefulLinkDAO.Create(usefulLink);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void RemoveUsefulLink(int id)
        {
            usefulLinkDAO.Delete(new UsefulLink { Id = id });
        }
    }
}