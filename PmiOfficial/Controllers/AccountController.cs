using PmiOfficial.Models;
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
        private readonly RegistrationService registrationService;
        // POST api/<controller>
        public IHttpActionResult Register([FromBody] RegistrationBindingModel model)
        {
            if (!this.ModelState.IsValid())
            {
                return BadRequest(this.ModelState);
            }
            RegistrationResult result = registrationService.Register(model);
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