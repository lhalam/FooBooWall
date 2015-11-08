using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Services.Registration;

namespace PmiOfficial.Controllers
{
    public class AccountController : ApiController
    {
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