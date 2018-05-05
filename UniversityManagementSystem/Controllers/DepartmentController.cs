using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager=new DepartmentManager();
        //
        // GET: /Department/
        public ActionResult Index()
        {
            List<Department> departments = (List<Department>) departmentManager.GetDepartments();
            return View(departments);
        }

        //
        // GET: /Department/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Department/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Department/Create
        [HttpPost]
        public ActionResult Create(Department aDepartment)
        {
            try
            {
                string message = departmentManager.Save(aDepartment);
                ViewBag.Mgs = message;
                return View();
            }
            catch
            {
                return View();
            }
        }

      
    }
}
