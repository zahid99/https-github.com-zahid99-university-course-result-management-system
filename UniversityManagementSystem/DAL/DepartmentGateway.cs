using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class DepartmentGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public IEnumerable<Department> GetDepartments()
        {
            try
            {
                connection=new SqlConnection(connectionString);
                string query = "SELECT * FROM Departments";
                command=new SqlCommand(query,connection);
                List<Department> departments = new List<Department>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Department department = new Department
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString()
                    };

                    departments.Add(department);
                }
                reader.Close();
                return departments;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect department", exception);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }


        public int Insert(Department aDepartment)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "INSERT INTO Departments VALUES(@code,@name)";
                command=new SqlCommand(query,connection);
                connection.Open();
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@code", aDepartment.Code.ToUpper());
                command.Parameters.AddWithValue("@name", aDepartment.Name);
               
               int rowAffected= command.ExecuteNonQuery();
                return rowAffected;

            }
            catch (Exception exception)
            {
                throw new Exception("Unable to save department", exception);
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public bool IsEXistCode(string code)
        {
            
                connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM Departments WHERE Code=@code";
                command = new SqlCommand(query, connection);
                connection.Open();
               
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@code", code);
                Department department = null;
                reader = command.ExecuteReader();


                bool isExist = reader.HasRows;
                reader.Close();
                return isExist;
            
        }

        public bool IsExistName(string name)
        {
            connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Departments WHERE Name=@name";
            command = new SqlCommand(query, connection);
            connection.Open();
            
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", name);
            Department department = null;
            reader = command.ExecuteReader();


            bool isExist = reader.HasRows;
            reader.Close();
            return isExist;
        }




       


    }
}