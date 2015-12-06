using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Services.Verification
{
    public class SendingBindingModel
    {
        [Required(ErrorMessage = @"Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = @"Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = @"Password is required.")]
        [MinLength(8, ErrorMessage = @" Password minimum length is 8")]
        public string Password { get; set; }
    }
}
