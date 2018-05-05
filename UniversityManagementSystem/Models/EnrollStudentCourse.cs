﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class EnrollStudentCourse
    {
        public int Id { get; set; }
        [DisplayName("Registration No")]
        public int StudentId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [DisplayName("Course")]
        public int CourseId { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrollDate { get; set; }
    }
}