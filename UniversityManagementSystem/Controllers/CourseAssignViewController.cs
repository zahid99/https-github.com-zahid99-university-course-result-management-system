using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseAssignViewController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        CourseAssignToTeacherManager courseAssignToTeacherManager=new CourseAssignToTeacherManager();
        public ActionResult Index()
        {
            IEnumerable<Department> departments = departmentManager.GetDepartments();
            ViewBag.Departments = departments;
            return View();
        }


        public JsonResult GetCourseInformationByDepartmentId(int departmentId)
        {
            IEnumerable<CourseAssignView> courseViewModels = courseAssignToTeacherManager.GetAllAssignCourse().ToList().FindAll(deptId => deptId.DepartmentId == departmentId);
            return Json(courseViewModels, JsonRequestBehavior.AllowGet);


        }

        //
        // GET: /CourseAssignView/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CourseAssignView/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CourseAssignView/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }
}
