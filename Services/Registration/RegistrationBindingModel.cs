using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PmiOfficial.Models
{
    public class RegistrationBindingModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}