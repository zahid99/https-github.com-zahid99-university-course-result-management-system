 using System;
using System.Collections.Generic;
 using System.ComponentModel;
 using System.ComponentModel.DataAnnotations;
 using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class ClassScheduleViewModel
    {
        public int Id { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Schedule { get; set; }       

    }
}