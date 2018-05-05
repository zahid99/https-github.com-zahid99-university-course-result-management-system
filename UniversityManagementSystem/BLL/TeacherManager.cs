using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class TeacherManager
    {
        TeacherGateway teacherGateway=new TeacherGateway();
        public string Save(Teacher teacher)
        {
            if(teacherGateway.IsEXistEmail(teacher.Email))
               {
                   return "Email address must be unique";
               }
            if (teacherGateway.Save(teacher) > 0)
            {
                return "Saved Sucessfully";
            }
            return "Failed to save";
        }


        public IEnumerable<Designation> GetDesignations()
        {
             return teacherGateway.GetDesignations();
        }

        public IEnumerable<Teacher> GetTeacher(int id)
        {
            return teacherGateway.GetTeacherByDepartmentId(id);
        }

        public IEnumerable<Teacher> GetAllTeacher()
        {
            return teacherGateway.GetAllTeacher();
        }
}
}