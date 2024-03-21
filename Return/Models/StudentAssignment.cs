using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Return.Models
{
    public class StudentAssignment
    {
        public int Id { get; set; }
        public virtual User Student { get; set; }
        public int StudentId { get; set; }
        public virtual User Teacher { get; set; }
        public int TeacherId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }
    }
}