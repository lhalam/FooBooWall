﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Entities
{
    public class User
    {
        private int id;
        public int ID { get { return id; } set { id = value; } }
        private string login;
        public string Login { get { return login; } set { login = value; } }

        public string Password { get; set; }

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

        public User()
        {
            Login = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            FB_ID = String.Empty;
            VK_ID = String.Empty;
            EMail = String.Empty;
        }
    }
}