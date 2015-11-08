<<<<<<< HEAD
﻿using DataAccess.Identity;
using Microsoft.AspNet.Identity;
using PmiOfficial.Models;
using Services;
using Services.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
=======
﻿using System.Threading.Tasks;
>>>>>>> 318b6ae6d38342be5a6ee09d47601d28ded2f735
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Services.Registration;

namespace PmiOfficial.Controllers
{
    public class AccountController : ApiController
    {
        [Route("a")]
        public async Task<int> Login(string userName, string userPassword)
        {
            CustomUserManager c = new CustomUserManager();
            var user = await c.FindAsync(userName, userPassword);
            if (user == null)
            {
                return 0;
            }
            var id = new ClaimsIdentity(new List<Claim>(), DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignIn(id);
            return 1;
            
        }
        // POST api/<controller>
        public async Task<IHttpActionResult> Register([FromBody] RegistrationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await RegistrationService.Register(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return GetErrorResult(result);
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
                    foreach (var error in result.Errors)
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