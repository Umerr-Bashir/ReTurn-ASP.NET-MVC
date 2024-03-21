using Return.Models;
using Return.ViewModels;
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
            User user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return Redirect("/Admin/Dashboard"); ////Implement js to show alert
        }
        public ActionResult DeleteTeacher(int Id)
        {
            User user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            ClassIncharge classIncharge = db.ClassIncharges.Where(x => x.TeacherId == Id).FirstOrDefault();
            db.ClassIncharges.Remove(classIncharge);
            user.RoleId = 3;
            db.SaveChanges();
            return RedirectToAction("ManageTeachers", "Admin"); ////Implement js to show alert
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
            dbStudentClassEnrollment.SectionId = studentClassEnrollment.SectionId;
            db.SaveChanges();

            return Redirect("/Admin/Dashboard");
        }

        //==================================ManageTeachers=====================================

        public ActionResult ManageTeachers(User user)
        {
            List<User> teachers = db.Users.Where(x => x.RoleId == 2).ToList();
            List<ClassIncharge> classIncharges = db.ClassIncharges.ToList();

            List<TeacherInchargeViewModel> teacherInchargeViewModels = new List<TeacherInchargeViewModel>();

            foreach (var teacher in teachers)
            {
                User user1 = db.Users.Where(x => x.Id == teacher.Id).FirstOrDefault();
                TeacherInchargeViewModel tivm = new TeacherInchargeViewModel
                {
                    TeacherId = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Address = teacher.Address,
                    CNICNumber = teacher.CNICNumber,
                };
                if (user1 != null)
                {
                    var classIncharge = db.ClassIncharges.Where(x => x.TeacherId == teacher.Id).FirstOrDefault();
                    tivm.Section = classIncharge?.Section.Name ?? "NA";
                    tivm.Class = classIncharge?.Section.Class.Name ?? "NA";
                }
                else
                {
                    tivm.Section = "NA";
                }
                teacherInchargeViewModels.Add(tivm);
            }
            string accessToken = "";
            if (Request.Cookies.Get("user-access-token") != null)
            {
                accessToken = Request.Cookies.Get("user-access-token").Value;
            }
            User dbUser = db.Users.Where(x => x.AccessToken == accessToken && x.RoleId == 1).FirstOrDefault();

            if (dbUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(teacherInchargeViewModels);
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
        public ActionResult ViewEnrolledStudent(int Id)
        {
            StudentClassEnrollment studentClassEnrollment = db.StudentClassEnrollments.Where(x => x.Id == Id).FirstOrDefault();
            return View(studentClassEnrollment);
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

            List<ClassInchargeViewModel> classInchargeViewModels = new List<ClassInchargeViewModel>();

            foreach (var section in sections)
            {
                ClassIncharge classIncharge = db.ClassIncharges.Where(x => x.SectionId == section.Id).FirstOrDefault();
                ClassInchargeViewModel civm = new ClassInchargeViewModel
                {
                    SectionId = section.Id,
                    SectionName = section.Name,
                    ClassName = section.Class.Name

                };
                if (classIncharge != null)
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
            string accessToken = "";
            if (Request.Cookies.Get("user-access-token") != null)
            {
                accessToken = Request.Cookies.Get("user-access-token").Value;
            }
            User dbUser = db.Users.Where(x => x.AccessToken == accessToken && x.RoleId == 1).FirstOrDefault();

            if (dbUser == null)
            {
                return RedirectToAction("Login", "Account");
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
            List<StudentClassEnrollment> studentClassEnrollment = db.StudentClassEnrollments.Where(x => x.SectionId == Id).ToList();
            return View(studentClassEnrollment);
        }
        public ActionResult DeleteClassroom(int Id)
        {
            List<StudentClassEnrollment> classEnrollments = db.StudentClassEnrollments.Where(x => x.SectionId == Id).ToList();
            ClassIncharge classIncharges = db.ClassIncharges.Where(x => x.SectionId == Id).FirstOrDefault();
            Section section = db.Sections.Where(x => x.Id == Id).FirstOrDefault();
            db.StudentClassEnrollments.RemoveRange(classEnrollments);
            db.Sections.Remove(section);
            if (classIncharges == null)
            {
                db.SaveChanges();
                return Redirect("/Admin/ManageClassrooms");
            }
            db.ClassIncharges.Remove(classIncharges);
            db.SaveChanges();
            return Redirect("/Admin/ManageClassrooms"); ////Implement JS to Show Alert
        }
        [HttpGet]
        public ActionResult EditClassroom(int Id)
        {
            ViewBag.Section = db.Sections.ToList();
            ViewBag.ClassIncharge = db.ClassIncharges.ToList();
            ClassIncharge classIncharge = db.ClassIncharges.Where(x=>x.SectionId==Id).FirstOrDefault();
            return View(classIncharge);
        }
        [HttpPost]
        public ActionResult EditClassroom(ClassIncharge classIncharge)
        {
            
            ClassIncharge dbClassIncharge = db.ClassIncharges.Where(x=>x.Id==classIncharge.Id).FirstOrDefault();

            dbClassIncharge.SectionId = classIncharge.SectionId;
            //dbClassIncharge.Section.Name = classIncharge.Section.Name;
            dbClassIncharge.TeacherId = classIncharge.TeacherId;
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
        public ActionResult ViewLectures(int Id)
        {
            List<Subject> subjects = db.Subjects.Where(x=>x.SectionId==Id).ToList();
            List<User> teachers = db.Users.Where(x => x.RoleId == 2).ToList();
            List<Time> times = db.times.ToList();

            List<SubjectListSubjectViewModel> subjectListSubjectViewModels = new List<SubjectListSubjectViewModel>();
            
            foreach (var subject in subjects)
            {
                SubjectListSubjectViewModel slsvm = new SubjectListSubjectViewModel
                {

                    Name = subject.Name,
                    DayName = subject.Day.DayName,
                    Timing = subject.Time.Timing,
                    TeacherId = subject.TeacherId,
                    SectionId = subject.Section.Id,
                    SubjectId = subject.Id
                    

                };
                if (teachers != null)
                {
                    User user = db.Users.Where(x => x.Id == subject.TeacherId).FirstOrDefault();
                    slsvm.FirstName = user.FirstName ?? "NA";
                    slsvm.LastName = user.LastName ?? "NA";
                        
                }
                else
                {
                    slsvm.FirstName = "NA";
                }
                subjectListSubjectViewModels.Add(slsvm);
            };
            return View(subjectListSubjectViewModels);
        }

        [HttpGet]
        public ActionResult AddLecture()
        {
            ViewBag.Day = db.days.ToList();
            ViewBag.User = db.Users.Where(x => x.RoleId == 2).ToList();
            ViewBag.Section = db.Sections.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddLecture(Subject subject)
        {
            db.Subjects.Add(subject);
            db.SaveChanges();

            return RedirectToAction("ManageClassrooms", "Admin");
        }
        public ActionResult DeleteLecture(int id)
        {
            Subject subject = db.Subjects.Where(x => x.Id == id).FirstOrDefault();
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
        //=================================ViewAssignments================================
        [HttpGet]
        public ActionResult AddTime()
        {
            ViewBag.Time = db.times.ToList();
            List<Time> times = db.times.ToList();
            return View(times);
        }
        [HttpPost]
        public ActionResult AddTime(Time time)
        {
            db.times.Add(time);
            db.SaveChanges();
            return Redirect("/Admin/AddTime");
        }
        public ActionResult DeleteTime(int id)
        {
            Time time = db.times.Where(x => x.Id == id).FirstOrDefault();
            db.times.Remove(time);
            db.SaveChanges();
            return Redirect("/Admin/AddTime");
        }
        [HttpGet]
        public ActionResult AddDay()
        {
            List<Day> days = db.days.ToList();
            return View(days);
        }
        [HttpPost]
        public ActionResult AddDay(Day day)
        {
            db.days.Add(day);
            db.SaveChanges();
            return Redirect("/Admin/AddDay");
        }
        public ActionResult DeleteDay(int id)
        {
            Day day = db.days.Where(x => x.Id == id).FirstOrDefault();
            db.days.Remove(day);
            db.SaveChanges();
            return Redirect("/Admin/AddDay");
        }
        public ActionResult ViewAssignment(int id)
        {
            List<ClassAssignment> classAssignment = db.ClassAssignment.Where(x => x.SectionId == id).ToList();
            return View(classAssignment);
        }
        public ActionResult DeleteAssignment(int id)
        {
            ClassAssignment classAssignment = db.ClassAssignment.Where(x => x.Id == id).FirstOrDefault();
            db.ClassAssignment.Remove(classAssignment);
            db.SaveChanges();

            return Redirect("/Admin/ManageClassrooms/ViewAssignment");
        }
    }

}