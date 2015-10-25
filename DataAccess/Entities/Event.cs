using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Entities
{
    public class Event
    {
        private int id;
        public int ID { get { return id; } set { id = value; } }
        private int image_id;
        public int Image_ID { get { return image_id; } set { image_id = value; } }
        private string name;
        public string Name { get { return name; } set { name = value; } }
        private string location;
        public string Location { get { return location; } set { location = value; } }
        private string description;
        public string Decription { get { return description; } set { description = value; } }
        private int organizer_id;
        public int Organizer_id { get { return organizer_id; } set { organizer_id = value; } }
        private DateTime time;
        public DateTime Time { get { return time; } set { time = value; } } 
    }
}