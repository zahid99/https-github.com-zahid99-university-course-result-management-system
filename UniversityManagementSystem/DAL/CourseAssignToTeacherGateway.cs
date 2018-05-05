using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class CourseAssignToTeacherGateway
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public int Save(CourseAssignToTeacher courseAssign)
        {

            connection = new SqlConnection(connectionString);
            //string query = "INSERT INTO Teacher VALUES(@Name,@Address,@Email,@Contact,@DesignationId,@DepartmentId,,@CreditToBeTaken,@CreditTaken)";
            string query = "INSERT INTO CourseAssignToTeacher VALUES('" + courseAssign.DepartmentId + "','" + courseAssign.TeacherId + "','" +
                           courseAssign.CourseId + "','" + 1 + "')";

            connection.Open();
            command = new SqlCommand(query, connection);
            

            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }


        public int UpdateAssignCourse()
        {

            connection = new SqlConnection(connectionString);
            //string query = "INSERT INTO Teacher VALUES(@Name,@Address,@Email,@Contact,@DesignationId,@DepartmentId,,@CreditToBeTaken,@CreditTaken)";
            string query = "UPDATE CourseAssignToTeacher SET IsActive='"+0+"'";
            connection.Open();
            command = new SqlCommand(query, connection);
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }


        public IEnumerable<CourseAssignToTeacher> GetAssignToTeachers()
        {

            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM CourseAssignToTeacher";
            command = new SqlCommand(query, connection);
            List<CourseAssignToTeacher> courses = new List<CourseAssignToTeacher>();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CourseAssignToTeacher course = new CourseAssignToTeacher();
                {
                    course.Id = Convert.ToInt32(reader["Id"].ToString());
                   
                    course.DepartmentId = (int)reader["DepartmentId"];
                    course.TeacherId = (int)reader["TeacherId"];
                    course.CourseId = (int)reader["CourseId"];
                    course.Status = (bool) reader["IsActive"];                   
                }
                courses.Add(course);
            }
            reader.Close();
            return courses;
        }


        public IEnumerable<CourseAssignView> GetAssignToTeachersByDepartment()
        {

            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM CourseAssignToTeacherView WHERE active='1'";
            command = new SqlCommand(query, connection);
            List<CourseAssignView> courses = new List<CourseAssignView>();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CourseAssignView course = new CourseAssignView();
                {
                   

                    course.DepartmentId = (int)reader["DepartmentId"];
                    course.Code = reader["code"].ToString();
                    course.Name =  reader["courseName"].ToString();
                    course.Semester = reader["semesterName"].ToString();
                    course.Teacher = reader["teacherName"].ToString();
                }
                courses.Add(course);
            }
            reader.Close();
            return courses;
        }
    }
}