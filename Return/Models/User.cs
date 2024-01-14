using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Return.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CNICNumber { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public int Salary { get; set; }
        public string AccessToken { get; set; }
        public virtual Class Class { get; set; }
        public int? ClassId { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

    } 
}