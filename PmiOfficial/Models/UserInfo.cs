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
<<<<<<< HEAD
        public DateTime ConnectedTime { get; set; }

        public List<string> ConnectionsIdList = new List<string>();
=======
        public string ConnectionId { get; set; }
        public int UserId { get; set; }
>>>>>>> e69b3341a9f15008c7757579401cac5d21d4a2a2
    }
}