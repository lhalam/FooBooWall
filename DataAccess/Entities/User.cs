using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Entities
{
    public class User : IUser<int>
    {
        private int id;
        public int Id { get { return id; } set { id = value; } }
        private string login;
        public string Login { get { return login; } set { login = value; } }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        private string email;
        public string EMail { get { return email; } set { email = value; } }
        private string firstName;
        public string FirstName { get { return firstName; } set { firstName = value; } }
        private string lastName;
        public string LastName { get { return lastName; } set { lastName = value; } }
        private DateTime birthday;
        public DateTime Birthday { get { return birthday; } set { birthday = value; } }
        private int image_id;
        public int Image_ID { get { return image_id; } set { image_id = value; } }
        private string fb_id;
        public string FB_ID { get { return fb_id; } set { fb_id = value; } }
        private string vk_id;
        public string VK_ID { get { return vk_id; } set { vk_id = value; } }
        public string Skype { get; set; }
        public string Hobbies { get; set; }
        public Dictionary<string, List<string>> Plans { get; set; }
        public string UserName
        {
            get
            {
                return this.Login;
            }
            set
            {
                this.Login = value;
            }
        }

        public User()
        {
            Login = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            FB_ID = Guid.NewGuid().ToString();
            VK_ID = Guid.NewGuid().ToString();
            EMail = String.Empty;
        }
    }
}