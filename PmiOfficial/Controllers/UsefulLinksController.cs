using DataAccess.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.DAO;
using Services;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace PmiOfficial.Controllers
{
    public class UsefulLinksController : ApiController
    {
        private readonly UsefulLinkService _usefulLinkService = new UsefulLinkService(new UsefulLinkDAO());
        
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
        public IHttpActionResult AddUsefulLink([FromBody]UsefulLinkDTO entity)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            UsefulLinkResult res = _usefulLinkService.Create(entity);
            if (res.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(res);
            }
        }

        private IHttpActionResult GetErrorResult(UsefulLinkResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void RemoveUsefulLink(int id)
        {
            _usefulLinkService.Delete(new UsefulLinkDTO() { Id = id });
        }
    }
}