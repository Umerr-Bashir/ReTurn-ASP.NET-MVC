using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Return.ViewModels
{
    public class TeacherInchargeViewModel
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Section { get; set; }
        public string Class { get; set; }
        public string Address { get; set; }
        public string CNICNumber { get; set; }

    }
}