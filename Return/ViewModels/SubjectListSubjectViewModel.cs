using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Return.ViewModels
{
    public class SubjectListSubjectViewModel
    {
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Timing { get; set; }
        public string DayName { get; set; }
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}