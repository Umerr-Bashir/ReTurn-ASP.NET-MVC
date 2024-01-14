using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Return.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult TeacherDashboard()
        {
            return View();
        }
    }
}