using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PmiOfficial.Models
{
    public class EventBindingModel
    {
        public string title { get; set; }
        public string start { get; set; }
        public int id { get; set; }

        public EventBindingModel(string inTitle, DateTime inTime, int inId)
        {
            title = inTitle;

            start = inTime.ToString("yyyy-MM-dd");
            id = inId;
        }
   
    }
}