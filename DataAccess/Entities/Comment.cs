using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int AuthorId { get;set;}
        public string AuthorName { get; set; }
        public string ImageName { get; set; }
        public DateTime WritingDate { get; set; }
        public string Text { get; set; }
    }
}
