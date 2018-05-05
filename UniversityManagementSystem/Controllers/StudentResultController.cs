using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class StudentResultController : Controller
    {
        StudentManager studentManager=new StudentManager();
        StudentResultManager studentResultManager=new StudentResultManager();
        public ActionResult Index()
        {
            ViewBag.Students = studentManager.GetStudents();
            return View();
        }

        //
        // GET: /StudentResult/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StudentResult/Create
        public ActionResult Save()
        {
            ViewBag.Students = studentManager.GetStudents();
            return View();
        }

        //
        // POST: /StudentResult/Create
        [HttpPost]
        public ActionResult Save(StudentResult studentResult)
        {
            try
            {
                ViewBag.Students = studentManager.GetStudents();
                ViewBag.message = studentResultManager.SaveStudentResult(studentResult);

                return View();
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetStudentById(int studentId)
        {
            StudentViewModel student = studentManager.GetStudentsById(studentId).ToList().Find(st => st.Id == studentId);
            //IEnumerable<Course> courses = courseManager.GetAllCourse().ToList().FindAll(d => d.DepartmentId == aStudent.DepartmentId);
            //return Json(courses, JsonRequestBehavior.AllowGet);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesTakebByStudent(int studentId)
        {
            //IEnumerable<Course> courses = courseManager.GetCoursesTakenByaStudentById(studentId);
            //return Json(courses, JsonRequestBehavior.AllowGet);

            //Course course = studentManager.GetEnrollStudentCourse(studentId).ToList().Find(st => st.c == studentId);
            IEnumerable<StudentResult> courses = studentManager.GetEnrollStudentCourse(studentId);
            //return Json(courses, JsonRequestBehavior.AllowGet);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetStudentResultByStudentId(int studentId)
        {
            
            var studentresult = studentResultManager.GetStudentResultViews();
            var studentresultList = studentresult.Where(t => t.StudentId == studentId).ToList();
           
            return Json(studentresultList);
        }

    }
}
