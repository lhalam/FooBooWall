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
        private DateTime birthDate;
        public DateTime BirthDate { get { return birthDate; } set { birthDate = value; } }
        private int image_id;
        private List<UsefulLink> usefulLinks;
        public List<UsefulLink> UsefulLinks { get { return usefulLinks; } set { usefulLinks = value; } }
        private List<InterestingVisit> interestingVisits;
        public List<InterestingVisit> InterestingVisits { get { return interestingVisits; } set { interestingVisits = value; } }
        private long phoneNumber;
        public long PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public int Image_ID { get { return image_id; } set { image_id = value; } }
        private string fb_id;
        public string FB_ID { get { return fb_id; } set { fb_id = value; } }
        private string vk_id;
        public string VK_ID { get { return vk_id; } set { vk_id = value; } }

    }
}