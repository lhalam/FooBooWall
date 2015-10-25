using PmiOfficial.Models;
using Services;
using Services.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PmiOfficial.Controllers
{
    public class AccountController : ApiController
    {
        // POST api/<controller>
        public IHttpActionResult Register([FromBody] RegistrationBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            RegistrationResult result = RegistrationService.Register(model);
            if (result.Succeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        private IHttpActionResult GetErrorResult(RegistrationResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeded)
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