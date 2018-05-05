using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseController : Controller
    {

        DepartmentManager aDepartmentManager=new DepartmentManager();
        CourseManager aCourseManager=new CourseManager();

        //
        // GET: /Course/
        public ActionResult Save()
        {
            return View();
        }

        

        //
        // GET: /Course/Create
        public ActionResult Create()
        {
            List<Department> departments = (List<Department>) aDepartmentManager.GetDepartments();
            List<Semester> semesters=(List<Semester>) aCourseManager.Getsemetsers();
            ViewBag.departments = departments;
            ViewBag.semesters = semesters;
            return View();
        }

        //
        // POST: /Course/Create
        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                List<Department> departments = (List<Department>)aDepartmentManager.GetDepartments();
                List<Semester> semesters = (List<Semester>)aCourseManager.Getsemetsers();
                ViewBag.departments = departments;
                ViewBag.semesters = semesters;
                ViewBag.message=aCourseManager.Save(course);
                //return RedirectToAction("Index");
                return View();
            }
            catch
            {               
                return View();
            }
        }

       
        
    }
}
