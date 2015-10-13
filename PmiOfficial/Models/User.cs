using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PmiOfficial.Models
{
    public class User
    {
        private int id;
        public int ID { get {return id;} set{id = value;}}
        private string login;
        public string Login { get { return login; } set { login = value; } }
        private string email;
        public string EMail { get { return email; } set { email = value; } }
        private string firstName;
        public string FirstName { get { return firstName; } set { firstName = value; } }
        private string lastName;
        public string LastName { get { return lastName; } set { lastName = value; } }
        private int age;
        public int Age { get { return age; } set { age = value; } }
        private int image_id;
        public int Image_ID { get { return image_id; } set { image_id = value; } }
        private int fb_id;
        public int FB_ID { get { return fb_id; } set { fb_id = value; } }
        private int vk_id;
        public int VK_ID { get { return vk_id; } set { vk_id = value; } }
    }
}