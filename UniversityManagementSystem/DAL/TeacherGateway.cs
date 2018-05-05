using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem.Models;
using WebGrease.Css.Ast;

namespace UniversityManagementSystem.DAL
{
    public class TeacherGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public int Save(Teacher teacher)
        {

            connection = new SqlConnection(connectionString);
            //string query = "INSERT INTO Teacher VALUES(@Name,@Address,@Email,@Contact,@DesignationId,@DepartmentId,,@CreditToBeTaken,@CreditTaken)";
            string query = "INSERT INTO Teacher VALUES('" + teacher.Name + "','" + teacher.Address + "','" +
                           teacher.Email + "','" + teacher.Contact + "','" + teacher.DesignationId + "','" +
                           teacher.DepartmentId + "','" + teacher.CreditTobeTaken + "','" + 0 + "')";

            connection.Open();
            command = new SqlCommand(query, connection);
            //command.Parameters.Clear();
            //command.Parameters.AddWithValue("@Name", teacher.Name);
            //command.Parameters.AddWithValue("@Address", teacher.Address);
            //command.Parameters.AddWithValue("@Email", teacher.Email.ToLower());
            //command.Parameters.AddWithValue("@Contact", teacher.Contact);
            //command.Parameters.AddWithValue("@DesignationId", teacher.DesignationId);
            //command.Parameters.AddWithValue("@DepartmentId", teacher.DepartmentId);
            //command.Parameters.AddWithValue("@CreditToBeTaken", teacher.CreditTobeTaken);
            //command.Parameters.AddWithValue("@CreditTaken", 0);

            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }



        public int UpdateTeacher( int id,double creditTaken)
        {
            connection = new SqlConnection(connectionString);
             string query = "UPDATE Teacher SET CreditTaken='"+creditTaken+"' WHERE id='"+id+"'";
            connection.Open();
            command = new SqlCommand(query, connection);
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }


        public IEnumerable<Designation> GetDesignations()
        {
            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Designation";
            command = new SqlCommand(query, connection);
            List<Designation> designations = new List<Designation>();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Designation designation = new Designation();
                {
                    designation.Id = Convert.ToInt32(reader["Id"].ToString());

                    designation.Title = reader["Title"].ToString();
                }

                designations.Add(designation);
            }
            reader.Close();
            return designations;
        }


        public IEnumerable<Teacher> GetTeacherByDepartmentId(int id)
        {

            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Teacher WHERE Id='" + id + "' ";
            command = new SqlCommand(query, connection);
            List<Teacher> teachers = new List<Teacher>();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                {
                    teacher.Id = Convert.ToInt32(reader["Id"].ToString());

                    teacher.Name = reader["Name"].ToString();
                    teacher.CreditTaken = (double) reader["CreditTaken"];
                }

                teachers.Add(teacher);
            }
            reader.Close();
            return teachers;
        }


        public IEnumerable<Teacher> GetAllTeacher()
        {

            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Teacher";
            command = new SqlCommand(query, connection);
            List<Teacher> teachers = new List<Teacher>();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                {
                    teacher.Id = Convert.ToInt32(reader["Id"]);

                    teacher.Name = reader["Name"].ToString();
                    teacher.DepartmentId = (int) reader["DepartmentId"];
                    teacher.Address = reader["Address"].ToString();
                    teacher.Contact = reader["Contact"].ToString();
                    teacher.CreditTaken = Convert.ToDouble(reader["CreditTaken"]);
                    teacher.CreditTobeTaken = Convert.ToDouble(reader["CreditToBeTaken"]);
                    teacher.DesignationId = (int) reader["DesignationId"];
                    teacher.Email = reader["Email"].ToString();
                    
                }
                teachers.Add(teacher); 
            }
            reader.Close();
            return teachers;
        }


        public bool IsEXistEmail(string email)
        {
            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Teacher WHERE Email=@email";
            command = new SqlCommand(query, connection);
            connection.Open();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@email", email);
            reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            return isExist;

        }

        //public IEnumerable<Teacher> GetTeacherById(int id)
        //{

        //    connection = new SqlConnection(connectionString);
        //    string query = "SELECT * FROM Teacher WHERE Id='"+id+"'";
        //    command = new SqlCommand(query, connection);
        //    List<Teacher> teachers = new List<Teacher>();
        //    connection.Open();
        //    reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        Teacher teacher = new Teacher();
        //        {
        //            teacher.Id = Convert.ToInt32(reader["Id"].ToString());

        //            teacher.Name = reader["Name"].ToString();
        //            teacher.DepartmentId = (int)reader["DepartmentId"];
        //            teacher.Address = reader["Address"].ToString();
        //            teacher.Contact = reader["Contact"].ToString();
        //            teacher.CreditTaken = Convert.ToDouble(reader["CreditTaken"]);
        //            teacher.DesignationId = (int)reader["DesignationId"];
        //            teacher.Email = reader["Email"].ToString();

        //        }
        //        teachers.Add(teacher);
        //    }
        //    reader.Close();
        //    return teachers;
        //}
    }
}