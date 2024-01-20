using Return.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Return.Controllers
{
    public class AdminController : Controller
    {
        ReturnContext db = new ReturnContext();
        // GET: Admin
        public ActionResult Dashboard(User user)
        {
            string accessToken = "";
            if (Request.Cookies.Get("user-access-token") != null)
            {
                accessToken = Request.Cookies.Get("user-access-token").Value;
            }
            User dbUser = db.Users.Where(x => x.AccessToken == accessToken && x.RoleId == 1).FirstOrDefault();

            if (dbUser == null)
            {
                return Redirect("/Account/Login");
            }
            List<User> Users = db.Users.ToList();
            return View(Users);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            user.RoleId = 3;
            user.AccessToken = DateTime.UtcNow.Ticks.ToString();
            db.Users.Add(user);
            db.SaveChanges();
            return Redirect("/Admin/Dashboard");
        }

        //=======================ACTIONS=========================
        public ActionResult View(int Id)
        {
            User users = db.Users.Where(x => x.Id == Id).FirstOrDefault();

            return View(users);
        }

	
        public ActionResult Delete(int Id)
        {
            User user = db.Users.Where(x=>x.Id == Id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return Redirect("/Admin/Dashboard"); ////Implement js to show alert
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.Role = db.Roles.ToList();
            User user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            return View(user); 
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            User dbUser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();

            dbUser.RoleId = user.RoleId;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.CNICNumber = user.CNICNumber;
            dbUser.Password = user.Password;
            dbUser.Address = user.Address;
            db.SaveChanges();
            
            return Redirect("/Admin/Dashboard");
        }
        [HttpGet]
        public ActionResult EnrolledStudentEdit(int Id)
        {
            ViewBag.User = db.Users.Where(x => x.RoleId == 3).ToList();
            ViewBag.Class = db.Classes.ToList();
            ViewBag.Section = db.Sections.ToList();
            StudentClassEnrollment dbStudentClassEnrollment = db.StudentClassEnrollments.Where(x => x.Id == Id).FirstOrDefault();
            return View(dbStudentClassEnrollment);
        }
        [HttpPost]
        public ActionResult EnrolledStudentEdit(StudentClassEnrollment studentClassEnrollment)
        {
            StudentClassEnrollment dbStudentClassEnrollment = db.StudentClassEnrollments.Where(x => x.Id == studentClassEnrollment.Id).FirstOrDefault();

            dbStudentClassEnrollment.ClassId = studentClassEnrollment.ClassId;
            dbStudentClassEnrollment.SectionId = studentClassEnrollment.SectionId;
            db.SaveChanges();

            return Redirect("/Admin/Dashboard");
        }

        //==================================ManageTeachers=====================================
        public ActionResult ManageTeachers(User user)
        {
            string accessToken = "";
            if (Request.Cookies.Get("user-access-token") != null)
            {
                accessToken = Request.Cookies.Get("user-access-token").Value;
            }
            User dbUser = db.Users.Where(x => x.AccessToken == accessToken && x.RoleId == 1).FirstOrDefault();

            if (dbUser == null)
            {
                return Redirect("/Account/Login");
            }
            List<User> Users = db.Users.Where(x => x.RoleId == 2).ToList();
            return View(Users);
        }
        //========================================ManageStudents===================================
        public ActionResult ManageStudents()
        {
            string accessToken = "";
            if (Request.Cookies.Get("user-access-token") != null)
            {
                accessToken = Request.Cookies.Get("user-access-token").Value;
            }
            User dbUser = db.Users.Where(x => x.AccessToken == accessToken && x.RoleId == 1).FirstOrDefault();

            if (dbUser == null)
            {
                return Redirect("/Account/Login");
            }
            List<StudentClassEnrollment> studentClassEnrollments = db.StudentClassEnrollments.ToList();

            return View(studentClassEnrollments);
        }
        public ActionResult DeleteEnrolledStudent(int Id)
        {
            StudentClassEnrollment studentClassEnrollment = db.StudentClassEnrollments.Where(x => x.Id == Id).FirstOrDefault();
            db.StudentClassEnrollments.Remove(studentClassEnrollment);
            db.SaveChanges();
            return Redirect("/Admin/ManageStudents"); ////Implement js to show alert
        }
        //===========================================ManageClassrooms===================================
        public ActionResult ManageClassrooms()
        {
            List<Section> sections = db.Sections.ToList();
            List<User> teacher = db.Users.Where(x => x.RoleId == 2).ToList();
            List<ClassIncharge> classIncharges = db.ClassIncharges.ToList();

            List<ClassInchargeViewModels> classInchargeViewModels = new List<ClassInchargeViewModels>();

            foreach (var section in sections)
            {
                ClassIncharge classIncharge = db.ClassIncharges.Where(x=>x.SectionId==section.Id).FirstOrDefault();
                ClassInchargeViewModels civm = new ClassInchargeViewModels
                {
                    SectionId = section.Id,
                    SectionName = section.Name,
                    ClassName = section.Class.Name

                };
                if (classIncharge!=null)
                {
                    var teachers = teacher.Where(x => x.Id == classIncharge.TeacherId).FirstOrDefault();
                    civm.FirstName = teachers.FirstName ?? "NA";
                    civm.LastName = teachers.LastName ?? "NA";
                }
                else
                {
                    civm.FirstName = "NA";
                }
                classInchargeViewModels.Add(civm);
                
            }
            return View(classInchargeViewModels);
        }
        //===================ACTIONS==================
        [HttpGet]
        public ActionResult AddToSection()
        {
            ViewBag.Class = db.Classes.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddToSection(Section section)
        {
            db.Sections.Add(section);
            db.SaveChanges();
            return Redirect("/Admin/ManageClassrooms");
        }
        [HttpGet]
        public ActionResult AddClass()
        {
            List<Class> classes = db.Classes.ToList();
            return View(classes);
        }
        [HttpPost]
        public ActionResult AddClass(Class @class)
        {
            db.Classes.Add(@class);
            db.SaveChanges();
            return Redirect("/Admin/ManageClassrooms");
        }
        public ActionResult ViewClassroom(int Id)
        {
            List<StudentClassEnrollment> studentClassEnrollment = db.StudentClassEnrollments.Where(x => x.SectionId==Id).ToList();
            return View(studentClassEnrollment);
        }
        public ActionResult DeleteClassroom(int Id)
        {
            //List<ClassIncharge> classIncharges = db.ClassIncharges.ToList();
            //ClassIncharge classIncharge = db.ClassIncharges.Where(x => x.SectionId == Id).FirstOrDefault();


            ClassIncharge classIncharges = db.ClassIncharges.Where(x => x.SectionId == Id).FirstOrDefault();
            Section section = db.Sections.Where(x => x.Id == Id).FirstOrDefault();
            db.Sections.Remove(section);
            db.ClassIncharges.Remove(classIncharges);
            db.SaveChanges();
            return Redirect("/Admin/ManageClassrooms"); ////Implement js to show alert
        }
        [HttpGet]
        public ActionResult EditClassroom(int Id)
        {
            Class @class= db.Classes.Where(x => x.Id == Id).FirstOrDefault();
            return View(@class);
        }
        [HttpPost]
        public ActionResult EditClassroom(Class @class)
        {
            Class dbClass = db.Classes.Where(x => x.Id == @class.Id).FirstOrDefault();

            dbClass.Name = @class.Name;
            //dbClass.SectionId = @class.SectionId;
            db.SaveChanges();
            return Redirect("/Admin/ManageClassrooms");
        }
        
        public ActionResult DeleteClass(int id)
        {
            Class @class = db.Classes.Where(x => x.Id == id).FirstOrDefault();
            db.Classes.Remove(@class);
            db.SaveChanges();
            return Redirect("/Admin/ManageClassrooms");
        }
        public ActionResult ViewLectures(int id)
        {
            List<Subject> subjects = db.Subjects.Where(x => x.SectionId == id).ToList();
            return View(subjects);
        }
        [HttpGet]
        public ActionResult AddLecture()
        {
            ViewBag.User = db.Users.Where(x => x.RoleId == 2).ToList();
            ViewBag.Section = db.Sections.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddLecture(Subject subject)
        {
            db.Subjects.Add(subject);
            db.SaveChanges();

            return Redirect("/Admin/ManageClassrooms");
        }
        public ActionResult DeleteLecture(int id)
        {
            Subject subject = db.Subjects.Where(x=>x.Id == id).FirstOrDefault();
            db.Subjects.Remove(subject);
            db.SaveChanges();

            return Redirect("/Admin/ManageClassrooms");
        }

        [HttpGet]
        public ActionResult AssignClassIncharge()
        {
            ViewBag.User = db.Users.Where(x => x.RoleId == 2).ToList();
            ViewBag.Section = db.Sections.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AssignClassIncharge(ClassIncharge classIncharge)
        {
            db.ClassIncharges.Add(classIncharge);
            db.SaveChanges();

            return Redirect("/Admin/ManageClassrooms");
        }

        [HttpGet]
        public ActionResult StudentEnrollment()
        {
            ViewBag.User = db.Users.Where(x => x.RoleId == 3).ToList(); 
            ViewBag.Class = db.Classes.ToList(); 
            ViewBag.Section = db.Sections.ToList(); 
            return View();
        }
        [HttpPost]
        public ActionResult StudentEnrollment(StudentClassEnrollment studentClassEnrollment)
        {
            db.StudentClassEnrollments.Add(studentClassEnrollment);
            db.SaveChanges();
            return Redirect("/Admin/ManageStudents");
        }
    }

}