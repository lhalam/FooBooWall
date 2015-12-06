using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Services.Verification
{
    public class VerificationBindingModel
    {
        [Required(ErrorMessage = @"Code is required")]
        public int Code { get; set; }
    }
}
