using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PmiOfficial.Models
{
    public class RegistrationBindingModel
    {
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = " Password minimum length is 8")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}