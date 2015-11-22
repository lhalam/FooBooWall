using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PmiOfficial.Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}