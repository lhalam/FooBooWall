using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PmiOfficial.Models
{
    public class Event
    {
        private int id;
        public int ID { get { return id; } set { id = value; } }
        private string name;
        public string Name { get { return name; } set { name = value; } }
        private string location;
        public string Location { get { return location; } set { location = value; } }
        private string description;
        public string Decription { get { return description; } set { description = value; } }
        private User organiser;
        public User Organiser { get { return organiser; } set { organiser = value; } }
        private DateTime time;
        public DateTime Time { get { return time; } set { time = value; } }
    }
}