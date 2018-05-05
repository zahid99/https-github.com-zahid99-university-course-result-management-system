using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;

namespace UniversityManagementSystem.Controllers
{
    public class UnAssignCourseController : Controller
    {
       

        //
        // GET: /UnAssignCourse/Create
        public ActionResult UnAssign()
        {
            return View();
        }

        //
        // POST: /UnAssignCourse/Create
        [HttpPost]
        public ActionResult UnAssign(int?  id)
        {
            try
            {
                CourseAssignToTeacherManager courseAssignToTeacher=new CourseAssignToTeacherManager();
                ViewBag.message = courseAssignToTeacher.UpdateAssignCourseTeacher();

                return View();
            }
            catch
            {
                return View();
            }
        }

        

       
    }
}
