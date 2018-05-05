using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class CourseGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public int Save(Course aCourse)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "INSERT INTO Course VALUES(@Code,@Name,@Credit,@Description,@DepartmentId,@SemesterId)";
                connection.Open();
                command = new SqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Code", aCourse.Code.ToUpper());
                command.Parameters.AddWithValue("@Name", aCourse.Name);
                command.Parameters.AddWithValue("@Credit", aCourse.Credit);
                command.Parameters.AddWithValue("@Description", aCourse.Description);
                command.Parameters.AddWithValue("@DepartmentId", aCourse.DepartmentId);
                command.Parameters.AddWithValue("@SemesterId", aCourse.SemesterId);
               
                return command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to Seve course", exception);
            }
            finally
            {
                connection.Close();
               
            } 
        }

        public bool IsEXistCode(string code)
        {
            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Course WHERE Code=@code";
            command = new SqlCommand(query, connection);
            connection.Open();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@code", code);          
            reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            return isExist;

        }

        public bool IsEXistName(string name)
        {
            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Course WHERE Name=@Name";
            command = new SqlCommand(query, connection);
            connection.Open();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", name);
            reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            return isExist;

        }

        public IEnumerable<Course> GetAllCourse()
        {

            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Course";
            command = new SqlCommand(query, connection);
            List<Course> courses = new List<Course>();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Course course = new Course();
                {
                    course.Id = Convert.ToInt32(reader["Id"].ToString());
                    course.Name = reader["Name"].ToString();
                    course.DepartmentId = (int)reader["DepartmentId"];
                    course.Code = reader["Code"].ToString();
                    course.Credit = (double)reader["Credit"];
                    course.SemesterId = (int)(reader["SemesterId"]);
                    //course.Description = reader["Description"].ToString();

                }
                courses.Add(course);
            }
            reader.Close();
            return courses;
        }
    }
}