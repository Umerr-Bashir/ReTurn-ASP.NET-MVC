using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Return.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Class Class { get; set; }
        public int ClassId { get; set; }


    }
}