using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class StudentManager
    {
        StudentGateway studentGateway=new StudentGateway();
        DepartmentGateway departmentGateway=new DepartmentGateway();
        public string Save(Student aStudent)
        {
            int counter;
            Department department = departmentGateway.GetDepartments().Single(depid => depid.Id == aStudent.DepartmentId);
            string searchKey = department.Code + "-" + aStudent.RegDate.Year + "-";
            string lastAddedRegistrationNo = studentGateway.GetRegNo(searchKey);
            if (lastAddedRegistrationNo == null)
            {
                aStudent.RegNo = searchKey + "001";

            }

            if (lastAddedRegistrationNo != null)
            {
                string tempId = lastAddedRegistrationNo.Substring((lastAddedRegistrationNo.Length - 3), 3);
                counter = Convert.ToInt32(tempId);
                string studentSl = (counter + 1).ToString();


                if (studentSl.Length == 1)
                {

                    aStudent.RegNo = searchKey + "00" + studentSl;

                }
                else if (studentSl.Count() == 2)
                {

                    aStudent.RegNo = searchKey + "0" + studentSl;
                }
                else
                {

                    aStudent.RegNo = searchKey + studentSl;
                }

            }
            //var listOfEmailAddress = from student in GetAll
            //                         select student.Email;
            //string tempEmail = listOfEmailAddress.ToList().Find(email => email.Contains(aStudent.Email));

            //if (tempEmail != null)
            //{
            //    return "Email address must be unique";
            //}
            //if (IsEmailAddressValid(aStudent.Email))
            //{


                if (studentGateway.IsExistEmail(aStudent.Email))
                {
                    return "Email Must be Unique";
                }
                if (studentGateway.Insert(aStudent) > 0)
                {
                    return "Saved Successfully!;Registration No:" + aStudent.RegNo + ";Name:" + aStudent.Name + ";Email:" + aStudent.Email + ";Contact Number:" + aStudent.Contact;
                }

                return "Failed to save";
            //}
            return "Please! enter a valid email address";
        }



        public IEnumerable<Student> GetStudents()
        {
            return studentGateway.GetStudentss();
        }

        public IEnumerable<StudentViewModel> GetStudentsById(int id)
        {
            return studentGateway.GetStudentsById(id);
        }

        public string SaveEnrollCourse(EnrollStudentCourse enrollStudentCourse)
        {

            if (studentGateway.IsEXistEnrollStudentCourse(enrollStudentCourse.CourseId,enrollStudentCourse.StudentId))
            {
                return " Exist Course";
            }
            if (studentGateway.SaveEnrollCourse(enrollStudentCourse)>0)
            {
                return "Save Enroll Course";
            }
            return "Failed to save";
        }



        public IEnumerable<StudentResult> GetEnrollStudentCourse(int studentId)
        {
            StudentResultGateway studentResultGateway=new StudentResultGateway();
            return studentResultGateway.GetStudentEnrollCourses(studentId);
        } 


    }
}