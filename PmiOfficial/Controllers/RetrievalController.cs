using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using DataAccess.DAO;
using DataAccess.Entities;
using Services.RetrievalPassword;



namespace PmiOfficial.Controllers
{
    public class RetrievalController : ApiController
    {
        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult GetCode([FromBody] EmailModelForRetrieval model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                UpdatingPasswordService service = new UpdatingPasswordService();
                if (service.SendNotificationLetter(model.Email))
                {
                    return Ok("Write code that you have received to email");
                }

                return BadRequest("Such user not exists");
            }
        }

        public IHttpActionResult ConfirmCode([FromBody] EmailCodeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                UpdatingPasswordService service = new UpdatingPasswordService();
                if (service.CheckCode(model.Code))
                {
                    return Ok("Input your new password");
                }
                return BadRequest("Codes mismatch");
            }
        }

        public IHttpActionResult ConfirmPassword([FromBody] NewPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                UpdatingPasswordService service = new UpdatingPasswordService();
                service.UpdatePassword(model);
                return Ok();
            }
        }
    }
}
