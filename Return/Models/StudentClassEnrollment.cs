using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Return.Models
{
    public class StudentClassEnrollment
    {
        public int Id { get; set; }
        public virtual User Student { get; set; }
        public int StudentId { get; set; }
        public virtual Section Section { get; set; }
        public int SectionId { get; set; }
    }
}