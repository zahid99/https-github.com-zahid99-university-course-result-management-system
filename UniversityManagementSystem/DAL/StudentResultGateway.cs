using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class StudentResultGateway
    {

        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public IEnumerable<StudentResult> GetStudentEnrollCourses(int studentId)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "select course.Id as id,Course.Code as code,Course.Name as name,StudentEnrollInCourse.StudentId as studentId from Course inner join StudentEnrollInCourse on Course.Id=StudentEnrollInCourse.CourseId where StudentEnrollInCourse.StudentId='"+studentId+"'";
                command = new SqlCommand(query, connection);
                List<StudentResult> courses = new List<StudentResult>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StudentResult course = new StudentResult();
                    {
                        course.Id = Convert.ToInt32(reader["id"].ToString());
                        course.StudentId = (int) reader["studentId"];                       
                        course.CourseCode = reader["code"].ToString();                        
                    };
                    courses.Add(course);
                }
                reader.Close();
                return courses;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect ", exception);
            }
            finally
            {
                connection.Close();
            }
        }


        public int SaveStudentResult(StudentResult studentResult)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                string query = "INSERT INTO t_StudentResult VALUES(@StudentId,@CourseId,@Grade,@Status)";
                command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@StudentId", studentResult.StudentId);
                command.Parameters.AddWithValue("@CourseId", studentResult.CourseId);
                command.Parameters.AddWithValue("@Grade", studentResult.Grade.ToUpper());
                command.Parameters.AddWithValue("@Status", studentResult.Status);
                
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Could Not save", exception);
            }
            finally
            {
                connection.Close();
            }
        }


        public bool IsEXistResult(int studentId,int courseId)
        {

            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM t_StudentResult WHERE StudentId=@studentId and CourseId=@code ";
            command = new SqlCommand(query, connection);
            connection.Open();

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@code", courseId);
            command.Parameters.AddWithValue("@studentId", studentId);           
            reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;

        }


        public IEnumerable<StudentResultView> GetStudentResult()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "SELECT *FROM StudentResultView";
                command = new SqlCommand(query, connection);
                List<StudentResultView> studentResultViews = new List<StudentResultView>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StudentResultView studentResult = new StudentResultView();
                    {
                        studentResult.StudentId = Convert.ToInt32(reader["studentId"].ToString());
                        studentResult.Code = reader["code"].ToString();
                        studentResult.Name = reader["courseName"].ToString();
                        studentResult.Grade = reader["grade"].ToString();
                    };
                    studentResultViews.Add(studentResult);
                }
                reader.Close();
                return studentResultViews;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect ", exception);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}