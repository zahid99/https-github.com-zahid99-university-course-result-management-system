using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class ClassSchedule
    {       
        public int Id { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
           [DisplayName("Room No")]
        public int RoomId { get; set; }     
        public string RoomNo { get; set; }
         [DisplayName("Day")]
        public int DayId { get; set; }       
        [DisplayName("Day")]
        public string DayName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}