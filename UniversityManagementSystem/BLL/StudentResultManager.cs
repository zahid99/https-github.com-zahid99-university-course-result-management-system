using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class StudentResultManager
    {
        StudentResultGateway studentResultGateway=new StudentResultGateway();

        public string SaveStudentResult(StudentResult studentResult)
        {
            if (studentResultGateway.IsEXistResult(studentResult.StudentId,studentResult.CourseId))
            {
                return "Student Result Already Assign";
            }
            if (studentResultGateway.SaveStudentResult(studentResult)>0)
            {
                return "Save Student Result";
            }
            return "Failed to Save";
        }

        public IEnumerable<StudentResultView> GetStudentResultViews()
        {
            return studentResultGateway.GetStudentResult();
        }
    }
}