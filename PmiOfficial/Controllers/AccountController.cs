
﻿using DataAccess.Identity;
﻿using DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PmiOfficial.Models;
using PmiOfficial.Results;
using Services;
using Services.Auth;
using Services.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace PmiOfficial.Controllers
{
    public class AccountController : ApiController
    {
        private readonly CustomUserManager userManager = new CustomUserManager();
        private readonly IAuthService _authService = new AuthService();

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public IHttpActionResult Logout()
        {
            LogOut();
            string urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            return Redirect(urlBase);
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Register([FromBody] RegistrationBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            IdentityResult result = await RegistrationService.Register(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }


        //
        // POST: /Account/Login

        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    SignIn(user);
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            string redirectUri = string.Empty;

            if (error != null)
            {
                return BadRequest(Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            UserLoginInfo info = new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey);
            User user = await _authService.ExternalAuthentication(externalLogin, info);
            if(user == null)
            {
                return InternalServerError();
            }
           
            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            SignIn(user);
            string urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            return Redirect(urlBase);
        }

        
        #region HelperMethods
        private IAuthenticationManager Authentication 
        { 
            get 
            {
                return this.Request.GetOwinContext().Authentication;
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

        private void SignIn(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Login));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer));
            var id = new ClaimsIdentity(claims,
                                        DefaultAuthenticationTypes.ApplicationCookie);
            Authentication.SignIn(new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }, id);
        }

        private void LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
        }

       

    }
        #endregion
}