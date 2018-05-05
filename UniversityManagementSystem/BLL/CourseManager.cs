using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class CourseManager
    {
        CourseGateway courseGateway=new CourseGateway();
       

        public string Save(Course aCourse)
        {
            //if (!(IsCorseCodeValid(aCourse)))
            //{
            //    return "Course code must be at least 5 character of length";
            //}
            if (courseGateway.IsEXistName(aCourse.Name))
            {
                return "Course Name Already Exists ! Course Name must be unique";
            }
            if (courseGateway.IsEXistCode(aCourse.Code))
            {
                return "Course Code Already Exists ! Code must be unique";
            }
            if (courseGateway.Save(aCourse) > 0)
            {
                return "Saved Successfully";
            }
            return "Failed to save";
        }


        public IEnumerable<Semester> Getsemetsers()
        {
            SemesterGateway semesterGateway=new SemesterGateway();
            return semesterGateway.GetSemester();
        }

        public IEnumerable<Course> GetAllCourse()
        {
            return courseGateway.GetAllCourse();
        }
    }
}