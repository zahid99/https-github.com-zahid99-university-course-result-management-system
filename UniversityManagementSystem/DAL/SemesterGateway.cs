using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class SemesterGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        public IEnumerable<Semester> GetSemester()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM Semester";
                command = new SqlCommand(query, connection);
                List<Semester> semesters = new List<Semester>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Semester semester = new Semester();
                    {
                        semester.Id = Convert.ToInt32(reader["Id"].ToString());
                        semester.Name = reader["Name"].ToString();

                    };

                    semesters.Add(semester);
                }
                reader.Close();
                return semesters;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Semester", exception);
            }
            finally
            {
                connection.Close();
              
            }
        }
    }
}