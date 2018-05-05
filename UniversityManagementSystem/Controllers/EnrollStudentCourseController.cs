using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class EnrollStudentCourseController : Controller
    {
       
        StudentManager studentManager=new StudentManager();
        CourseManager courseManager=new CourseManager();
        public ActionResult Index()
        {
            return View();
        }
     
        //
        // GET: /EnrollStudentCourse/Create
        public ActionResult Create()
        {
            IEnumerable<Student> students = studentManager.GetStudents();
            ViewBag.Students = students;
            return View();
        }

        //
        // POST: /EnrollStudentCourse/Create
        [HttpPost]
        public ActionResult Create(EnrollStudentCourse enrollStudentCourse)
        {
            try
            {
                IEnumerable<Student> students = studentManager.GetStudents();
                ViewBag.Students = students;
                ViewBag.message = studentManager.SaveEnrollCourse(enrollStudentCourse);
                return View();
            }
            catch
            {
                return View();
            }
        }


        public JsonResult GetStudentById(int studentId)
        {
            //var students = studentManager.GetStudents();
            //var student = students.Where(t => t.Id == studentId).ToList();           
            //return Json(student);
            StudentViewModel student = studentManager.GetStudentsById(studentId).ToList().Find(st => st.Id == studentId);
         
           // var student = studentManager.GetStudentsById(studentId);
            //StudentViewModel student = studentManager.GetStudentInformationById(studentId);
            return Json(student);
        }

        public JsonResult GetCourseByStudentId(int studentId)
        {
            Student aStudent = studentManager.GetStudents().ToList().Find(st => st.Id == studentId);
            IEnumerable<Course> courses = courseManager.GetAllCourse().ToList().FindAll(d => d.DepartmentId == aStudent.DepartmentId);
            return Json(courses, JsonRequestBehavior.AllowGet);

        }

    }
}
