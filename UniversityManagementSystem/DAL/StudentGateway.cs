using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Microsoft.Owin.BuilderProperties;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class StudentGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public int Insert(Student aStudent)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                string query = "INSERT INTO Student VALUES(@RegNo,@Name,@Email,@ContactNo,@RegisterationDate,@Address,@DepartmentId)";
                command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@RegNo", aStudent.RegNo);
                command.Parameters.AddWithValue("@Name", aStudent.Name);
                command.Parameters.AddWithValue("@Email", aStudent.Email.ToLower());
                command.Parameters.AddWithValue("@ContactNo", aStudent.Contact);
                command.Parameters.AddWithValue("@RegisterationDate", aStudent.RegDate.ToShortDateString());
                command.Parameters.AddWithValue("@Address", aStudent.Address);
                command.Parameters.AddWithValue("@DepartmentId", aStudent.DepartmentId);
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


        public string GetRegNo(string searchKey)
        {
            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Student st WHERE RegNo LIKE '%" + searchKey + "%' and Id=(select Max(Id) FROM Student st WHERE RegNo LIKE '%" + searchKey + "%' )";
            command = new SqlCommand(query, connection);
            connection.Open();
            Student aStudent = null;
            string regNo = null;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                aStudent = new Student
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    RegNo = reader["RegNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Contact = reader["ContactNo"].ToString(),

                };
                regNo = aStudent.RegNo;
            }

            connection.Close();           
            reader.Close();
            return regNo;
        }


        public IEnumerable<Student> GetStudentss()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM Student";
                command = new SqlCommand(query, connection);
                List<Student> students = new List<Student>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();
                    {
                        student.Id = Convert.ToInt32(reader["Id"].ToString());
                        student.RegNo = reader["RegNo"].ToString();
                        student.Name = reader["Name"].ToString();
                        student.Email = reader["Email"].ToString();
                        student.Address = reader["Address"].ToString();
                        student.Contact = reader["ContactNo"].ToString();
                        student.RegDate = Convert.ToDateTime(reader["RegisterationDate"].ToString());
                        student.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                    };
                    students.Add(student);
                }
                reader.Close();
                return students;
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

        public IEnumerable<StudentViewModel> GetStudentsById(int id)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "select Student.ID as id,Student.Name as Name,Student.Email as Email,Departments.Name as deptName from Student,Departments WHERE Student.DepartmentId=Departments.Id And Student.Id='" + id + "';";
                command = new SqlCommand(query, connection);
                List<StudentViewModel> students = new List<StudentViewModel>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StudentViewModel student = new StudentViewModel();
                    {
                        student.Id = Convert.ToInt32(reader["id"].ToString());
                        //student.RegNo = reader["RegNo"].ToString();
                        student.Name = reader["Name"].ToString();
                        student.Email = reader["Email"].ToString();
                        //student.Address = reader["Address"].ToString();
                        //student.ContactNo = reader["ContactNo"].ToString();
                        //student.RegisterationDate = Convert.ToDateTime(reader["RegisterationDate"].ToString());
                        student.Department = (reader["deptName"].ToString());
                    };
                    students.Add(student);
                }
                reader.Close();
                return students;
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

        public int SaveEnrollCourse(EnrollStudentCourse enrollStudentCourse)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                //string query = "INSERT INTO StudentEnrollInCourse VALUES(@StudentId,@CourseId,@EnrollDate)";
                string query = "INSERT INTO StudentEnrollInCourse VALUES('" + enrollStudentCourse.StudentId + "','" + enrollStudentCourse.CourseId + "','" +
                          enrollStudentCourse.EnrollDate + "','"+1+"')";
                command = new SqlCommand(query, connection);                
                connection.Open();
                command = new SqlCommand(query, connection);
                 int rowAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowAffected;
                //command.Parameters.Clear();
                //command.Parameters.AddWithValue("@StudentId", enrollStudentCourse.StudentId);
                //command.Parameters.AddWithValue("@CourseId", enrollStudentCourse.Name);
                //command.Parameters.AddWithValue("@EnrollDate", enrollStudentCourse.EnrollDate);
                
                //command.Parameters.AddWithValue("@Status",1);
                //int rowAffected = command.ExecuteNonQuery();
                //return rowAffected;
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

        public bool IsEXistEnrollStudentCourse(int courseId, int studentId)
        {

            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM StudentEnrollInCourse WHERE courseId=@courseId AND StudentId=@studentId";
            command = new SqlCommand(query, connection);
            connection.Open();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@courseId", courseId);
            command.Parameters.AddWithValue("@studentId", studentId);
            Department department = null;
            reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            return isExist;

        }

        public bool IsExistEmail(string email)
        {
            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Student WHERE Email=@email";
            command = new SqlCommand(query, connection);
            connection.Open();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@email", email);                 
            reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            return isExist;

        }

    }
}