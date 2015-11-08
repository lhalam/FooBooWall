using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class ExternalLogin
    {
        public int ExternalLoginId { get; set; }
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
