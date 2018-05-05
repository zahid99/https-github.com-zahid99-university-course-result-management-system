using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class ClassRoomAllocateController : Controller
    {
        DepartmentManager departmentManager=new DepartmentManager();
        CourseManager courseManger = new CourseManager();
        ClassScheduleManager classScheduleManager=new ClassScheduleManager();
        public ActionResult Index()
        {
            IEnumerable<Department> departments = departmentManager.GetDepartments();
            ViewBag.Departments = departments;
            return View();
        }



        public ActionResult UnAllocate()
        {
           
            return View();
        }
      

     
        public ActionResult Save()
        {
            IEnumerable<Department> departments = departmentManager.GetDepartments();
            ViewBag.Departments = departments;
            IEnumerable<Day> days = classScheduleManager.GetDays();
            ViewBag.Days = days;
            IEnumerable<ClassRoom> classRooms = classScheduleManager.GetClassRoom();
            ViewBag.RoomList = classRooms;
            return View();
        }

        //
        // POST: /ClassRoomAllocate/Create
        [HttpPost]
        public ActionResult Save(ClassSchedule classSchedule)
        {
            try
            {
                IEnumerable<Department> departments = departmentManager.GetDepartments();
                ViewBag.Departments = departments;
                IEnumerable<Day> days = classScheduleManager.GetDays();
                ViewBag.Days = days;

                IEnumerable<ClassRoom> classRooms = classScheduleManager.GetClassRoom();
                ViewBag.RoomList = classRooms;

                ViewBag.message = classScheduleManager.SaveClassSchedule(classSchedule);
                return View();
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            var courses = courseManger.GetAllCourse();
            var courseList = courses.Where(c => c.DepartmentId == departmentId);
            return Json(courseList);
        }

        public JsonResult GetClassScheduleByDepartment(int departmentId)
        {
            var clsSches = classScheduleManager.GetClassSchedule(departmentId);         
            return Json(clsSches, JsonRequestBehavior.AllowGet);
        }

        
    }
}
