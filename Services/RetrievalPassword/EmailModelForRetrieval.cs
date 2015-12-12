using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RetrievalPassword
{
    public class EmailModelForRetrieval
    {
        [Required(ErrorMessage = "@Email is required")]
        public string Email { get; set; }
    }
}
