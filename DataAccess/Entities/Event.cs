using System;

namespace DataAccess.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Decription { get; set; }

        public int OrganizerId { get; set; }

        public DateTime Time { get; set; }
    }
}