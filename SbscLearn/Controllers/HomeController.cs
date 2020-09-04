using SbscLearn.Entities;
using SbscLearn.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SbscLearn.Controllers
{
    public class HomeController : Controller
    {
        private SbscLearnEntities db = new SbscLearnEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCourse()
        {
            return View("CreateCourse");
        }

        [HttpPost]
        public ActionResult CreateCourse(HttpPostedFileBase file, string CourseName, string CourseId)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);

                    Course course = new Course();
                    course.CourseName = CourseName;
                    course.CourseId = CourseId;
                    course.FileName = _path;

                    db.Courses.Add(course);
                    db.SaveChanges();
                }
                ViewBag.Message = "CourseAdded";
                return View("CreateCourse");
            }
            catch
            {
                ViewBag.Message = "CourseFailed";
                return View("CreateCourse");
            }
        }

        public ActionResult ListUsers()
        {
            var users = from c in db.CourseAttempts
                        join u in db.Users on c.UserId equals u.Id
                        group c by  new { u.FirstName, u.LastName, c.CourseId, c.Score } into g
                        select new UsersListModel
                                   {
                                        FirstName = g.Key.FirstName,
                                        LastName = g.Key.LastName,
                                        CourseId = g.Key.CourseId,
                                        Score = g.Max(s => s.Score.Value)
                                    };

            return View("ListUsers", users);
        }
    }
}