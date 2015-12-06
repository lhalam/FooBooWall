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

    public class UserInfoHub
    {
        public string Name { get; set; }
        public DateTime ConnectedTime { get; set; }

        public List<string> ConnectionsIdList = new List<string>();
    }
}