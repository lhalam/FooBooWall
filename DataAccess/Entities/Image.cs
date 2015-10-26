using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Entities
{
    public class Image
    {
        private int id;
        public int ID { get { return id; } set { id = value; } }
        private string name;
        public string Name { get { return name; } set { name = value; } }
    }
}