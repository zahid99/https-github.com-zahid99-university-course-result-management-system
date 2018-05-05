using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class TeacherController : Controller
    {

        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        //
        // GET: /Teacher/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Teacher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Teacher/Create
        public ActionResult Create()
        {
          

            ViewBag.departmentList = departmentManager.GetDepartments();
            ViewBag.designationList = teacherManager.GetDesignations();
            return View();
        }

        //
        // POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            try
            {
                List<Department> departments = (List<Department>) departmentManager.GetDepartments();
                List<Designation> designations = (List<Designation>) teacherManager.GetDesignations();

                ViewBag.departmentList = departments;
                ViewBag.designationList = designations;

                ViewBag.message = teacherManager.Save(teacher);

                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Teacher/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Teacher/Edit/5
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
        // GET: /Teacher/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Teacher/Delete/5
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
