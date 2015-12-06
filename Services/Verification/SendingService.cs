using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Identity;
using DataAccess.DAO;
using Microsoft.AspNet.Identity;

namespace Services.Verification
{
    public class SendingService
    {
        public static Task<IdentityResult> Send(SendingBindingModel model)
        {
            VerificationLetter vf = new VerificationLetter();
                vf.Login = model.Login;
                vf.Email = model.Email;
            VerificationDAO vd = new VerificationDAO();
            vd.Create(vf);

            IdentityResult ir = new IdentityResult();
            List<string> errors = new List<string>();
            if (model.Login == "" || model.Login == null)
            {
                errors.Add("Login required");
            }
            if (model.Password == "" || model.Password == null)
            {
                errors.Add("Password required");
            }
            else
            if (model.Password.Length < 8)
            {
                errors.Add("Password should contain at least 8 characters");
            }
            if (model.Email == "" || model.Email == null)
            {
                errors.Add("Email required");
            }
            else if (!(model.Email.Contains(".")&&model.Email.Contains("@")))
            {
                errors.Add("Invalid email format");
            }
            if (errors.Count != 0) ir = new IdentityResult(errors);
            else ir = IdentityResult.Success;
            return Task.FromResult(ir);
        }
    }
}
