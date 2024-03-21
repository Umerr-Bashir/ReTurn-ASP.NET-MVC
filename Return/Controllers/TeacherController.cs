using Return.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Return.Controllers
{
    public class TeacherController : Controller
    {
        ReturnContext db = new ReturnContext();
        // GET: Teacher
        public ActionResult Dashboard()
        {

            return View();
        }
    }
}