using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class CourseAssignToTeacherManager
    {
        CourseAssignToTeacherGateway courseAssignToTeacherGateway=new CourseAssignToTeacherGateway();
        TeacherGateway teacherGateway=new TeacherGateway();
        public string Save(CourseAssignToTeacher courseAssign)
        {

           // CourseAssignToTeacher courseAssignTo = courseAssignToTeacherGateway.GetAssignToTeachers().ToList().Find(ca => ca.CourseId == courseAssign.CourseId && ca.Status);
            var courseAssignTo = courseAssignToTeacherGateway.GetAssignToTeachers();
            var courseAssignList = courseAssignTo.Where(t => t.CourseId == courseAssign.CourseId).ToList();
            if (courseAssignList.Count==0)
            {
                if (courseAssignToTeacherGateway.Save(courseAssign) > 0)
                {
                    double credit = courseAssign.Credit + courseAssign.RemaingCredit;
                    int rowAffected = teacherGateway.UpdateTeacher(courseAssign.TeacherId, credit);
                    return "Assigned successfully";
                }
                return "Failed to save";
            }

            return "Overlaping not allowed!";
        }

        public IEnumerable<CourseAssignView> GetAllAssignCourse()
        {
        
            return courseAssignToTeacherGateway.GetAssignToTeachersByDepartment();
        
         }

        public string UpdateAssignCourseTeacher()
        {
            if (courseAssignToTeacherGateway.UpdateAssignCourse()>0)
            {
                return "UnAssign All Course Successfully";
            }
            return "Failed To Unassign";
        }

    }
}