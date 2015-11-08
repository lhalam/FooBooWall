using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace DataAccess.Entities
{
    public class User : IUser<int>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int ImageId { get; set; }
        public string FbId { get; set; }
        public string VkId { get; set; }
        public string Skype { get; set; }
        public string Hobbies { get; set; }
        public Dictionary<string, List<string>> Plans { get; set; }
        public string UserName
        {
            get
            {
                return Login;
            }
            set
            {
                Login = value;
            }
        }

        public User()
        {
            Login = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            FbId = Guid.NewGuid().ToString();
            VkId = Guid.NewGuid().ToString();
            EMail = String.Empty;
        }
    }
}