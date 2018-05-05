using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class ClassScheduleGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public IEnumerable<Day> GetDays()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM Day";
                command = new SqlCommand(query, connection);
                List<Day> days = new List<Day>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Day day = new Day();
                    {
                        day.Id = Convert.ToInt32(reader["Id"].ToString());
                        day.Name = reader["Name"].ToString();                     
                    }
                    days.Add(day);
                }
                reader.Close();
                return days;
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

        public IEnumerable<ClassRoom> GetClassRoom()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM Room";
                command = new SqlCommand(query, connection);
                List<ClassRoom> classRooms = new List<ClassRoom>();
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClassRoom classRoom = new ClassRoom();
                    {
                        classRoom.Id = Convert.ToInt32(reader["Id"].ToString());
                        classRoom.Name = reader["Name"].ToString();
                    }
                    classRooms.Add(classRoom);
                }
                reader.Close();
                return classRooms;
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

        public int SaveClassSchedule(ClassSchedule classSchedule)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "INSERT INTO AllocateClassRoom VALUES(@DeptId,@CourseId,@RoomId,@DayId,@StartTime,@EndTime,@Status)";
                connection.Open();
                command = new SqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@DeptId", classSchedule.DepartmentId);
                command.Parameters.AddWithValue("@CourseId", classSchedule.CourseId);
                command.Parameters.AddWithValue("@RoomId", classSchedule.RoomId);
                command.Parameters.AddWithValue("@DayId", classSchedule.DayId);
                command.Parameters.AddWithValue("@StartTime", classSchedule.StartTime.ToShortTimeString());
                command.Parameters.AddWithValue("@EndTime", classSchedule.EndTime.ToShortTimeString());
                command.Parameters.AddWithValue("@Status", 1);

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


        public IEnumerable<ClassScheduleViewModel> GetClassSchedule(int departmentId)
        {

            connection = new SqlConnection(connectionString);
            string query = "select Code,name, COALESCE(NameValues,'Not Schedule Yet')as schedule from ClassSchedule where DepartmentId='"+departmentId+"'";
            command = new SqlCommand(query, connection);
            List<ClassScheduleViewModel> courses = new List<ClassScheduleViewModel>();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ClassScheduleViewModel course = new ClassScheduleViewModel();
                {
                    //course.Id = Convert.ToInt32(reader["Id"].ToString());
                    course.Code = (string) reader["Code"];
                    course.Name = (string) reader["name"];
                    course.Schedule = (string) reader["schedule"];
                    
                }
                courses.Add(course);
            }
            reader.Close();
            return courses;
        }
    }
}