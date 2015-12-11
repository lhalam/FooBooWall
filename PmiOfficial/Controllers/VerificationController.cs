using DataAccess.Identity;
using DataAccess.Entities;
using DataAccess.DAO;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PmiOfficial.Models;
using PmiOfficial.Results;
using Services;
using Services.Auth;
using Services.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using PmiOfficial.Filters;

namespace PmiOfficial.Controllers
{
    public class VerificationController : ApiController
    {
        // POST api/<controller>
        public async Task<IHttpActionResult> Send([FromBody] SendingBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            IdentityResult result = await SendingService.Send(model);
            VerificationDAO vd = new VerificationDAO();
            VerificationLetter vlt = vd.Read(1);
            if (result.Succeeded)
            {
                vlt.Send();
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }
        public async Task<IHttpActionResult> Confirm([FromBody] VerificationBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            await VerificationService.Send(model);
            VerificationDAO vd = new VerificationDAO();
            VerificationLetter vlt = vd.Read(1);
            if (model.Code == vlt.Code)
            {
                vd.Update(vlt);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        private IHttpActionResult GetErrorResult(IdentityResult result)
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
    }
}
