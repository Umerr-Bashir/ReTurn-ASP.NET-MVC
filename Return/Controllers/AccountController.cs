using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Return.Models;
namespace Return.Controllers
{
    public class AccountController : Controller
    {
        ReturnContext db = new ReturnContext();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            //string accessToken = "";
            //if (Request.Cookies.Get("user-access-token") != null)
            //{
            //    accessToken = Request.Cookies.Get("user-access-token").Value;
            //}
            //User dbUser = db.users.Where(x => x.AccessToken == accessToken && (x.RoleId == 1 ||
            //x.RoleId == 2|| x.RoleId == 3)).FirstOrDefault();
            //if (dbUser.RoleId==1)
            //{
            //    return Redirect("/Admin/Dashboard");
            //}
            //else if (dbUser.RoleId==2)
            //{
            //    return Redirect("/Teacher/TeacherPanel");
            //}
            //else if (dbUser.RoleId==3)
            //{
            //    return Redirect("/Student/StudentPanel");
            //}
            //else
            //{
            //    return Redirect("/Account/Login");
            //}

            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            User dbUser = db.Users.Where(x=>x.Email==user.Email && x.Password==user.Password).FirstOrDefault();
            if (dbUser==null)
            {
                ViewBag.error = "Your Email or Password is Incorrect";return View();
            }
            HttpCookie mycookie = new HttpCookie("user-access-token");
            mycookie.Value = dbUser.AccessToken;
            mycookie.Expires = DateTime.UtcNow.AddDays(15).AddHours(5);
            Response.Cookies.Remove("user-access-token");
            Response.Cookies.Add(mycookie);
            return Redirect("/Admin/Dashboard");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            user.RoleId = 3;
            user.AccessToken = DateTime.UtcNow.Ticks.ToString();
            db.Users.Add(user);
            db.SaveChanges();
            HttpCookie mycookie = new HttpCookie("user-access-token");
            mycookie.Value = user.AccessToken;
            mycookie.Expires = DateTime.UtcNow.AddDays(15).AddHours(5);
            Response.Cookies.Remove("user-access-token");
            Response.Cookies.Add(mycookie); 
            return Redirect("/Admin/Dashboard");
        }
        public ActionResult Logout(User user)
        {
            if (Request.Cookies["user-access-token"] != null)
            {Response.Cookies["user-access-token"].Expires = DateTime.UtcNow.AddHours(5).AddDays(-1);
             return Redirect("/Account/Login");
            }
            return View();
        }
    }
}