using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        StudentManager studentManager=new StudentManager();
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Student/Create
        public ActionResult Save()
        {
            IEnumerable<Department> departments = departmentManager.GetDepartments();
            
            ViewBag.Departments = departments;
            return View();
        }

        //
        // POST: /Student/Create
        [HttpPost]
        public ActionResult Save(Student aStudent)
        {
            try
            {

                ViewBag.Message = studentManager.Save(aStudent);
                IEnumerable<Department> departments = departmentManager.GetDepartments();
                ViewBag.Departments = departments;
                IEnumerable<Student> students = studentManager.GetStudents();
                ViewBag.Students = students;
                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
