using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseAssignToTeacherController : Controller
    {
        CourseManager courseManger = new CourseManager();
        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        CourseAssignToTeacherManager courseAssignToTeacherManager=new CourseAssignToTeacherManager();
       //CourseAssignToTeacherManager courseAssignToTeacherManager = new CourseAssignToTeacherManager();
     
        
        public ActionResult CourseAssign()
        {
            //IEnumerable<Course> courses = courseManger.GetAll;
            IEnumerable<Department> departments = departmentManager.GetDepartments();
            IEnumerable<Teacher> teachers = teacherManager.GetAllTeacher();
            //ViewBag.Courses = courses;

            
            ViewBag.Departments = departments;
            ViewBag.Teachers = teachers;
            return View();
        }
        [HttpPost]
        public ActionResult CourseAssign(CourseAssignToTeacher courseAssign)
        {
            try
            {                
                //ViewBag.Message = courseAssignToTeacherManager.Save(courseAssign);
                //IEnumerable<Course> courses = courseManger.GetAll;

                IEnumerable<Department> departments = departmentManager.GetDepartments();
                IEnumerable<Teacher> teachers = teacherManager.GetAllTeacher();
                //ViewBag.Courses = courses;
                ViewBag.Departments = departments;
                ViewBag.Teachers = teachers;
                string message=courseAssignToTeacherManager.Save(courseAssign);
                ViewBag.message = message;
                
                return View();
            }
            catch (Exception exception)
            {

                //ViewBag.Message = exception.InnerException.Message;
                return View();
            }


        }

        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {
            var teachers = teacherManager.GetAllTeacher();
            var teacherList = teachers.Where(t => t.DepartmentId == departmentId).ToList();
          
            return Json(teacherList);


        }

        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
           var courses = courseManger.GetAllCourse();
            var courseList = courses.Where(c => c.DepartmentId == departmentId);
            return Json(courseList);
        }

        public JsonResult GetTeacherById(int teacherId)
        {
            var teachers = teacherManager.GetAllTeacher();
            var aTeacher = teachers.FirstOrDefault(t => t.Id == teacherId);
            return Json(aTeacher);
        }

        public JsonResult GetCourseById(int courseId)
        {
           var courses = courseManger.GetAllCourse();
            var aCourse = courses.FirstOrDefault(c => c.Id == courseId);
            return Json(aCourse);
        }

        
    }
}
