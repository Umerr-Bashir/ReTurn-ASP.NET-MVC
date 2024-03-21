using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Return.Models
{
    public class ClassAssignment
    {
        public int Id { get; set; }
        public virtual User Teacher { get; set; }
        public int TeacherId { get; set; }
        public virtual Section Section { get; set; }
        public int SectionId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }

    }
}