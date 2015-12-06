using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Decription { get; set; }

        public DateTime Time { get; set; }
    }
}
