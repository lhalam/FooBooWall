using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Verification
{
    public class VerificationService
    {
        public static Task Send(VerificationBindingModel model)
        {
            return Task.FromResult(1);
        }
    }
}
