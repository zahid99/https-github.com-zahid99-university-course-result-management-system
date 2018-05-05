using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class ClassScheduleManager
    {
        ClassScheduleGateway classScheduleGateway=new ClassScheduleGateway();
        public IEnumerable<Day> GetDays()
        {
            return classScheduleGateway.GetDays();
        }

        public IEnumerable<ClassRoom> GetClassRoom()
        {
            return classScheduleGateway.GetClassRoom();
        }

        public string SaveClassSchedule(ClassSchedule classSchedule)
        {
            if (classScheduleGateway.SaveClassSchedule(classSchedule)>0)
            {
                return "Allocate Class Room";
            }
            return "Failed to Allocate";
        }


        public IEnumerable<ClassScheduleViewModel> GetClassSchedule(int departmentId)
        {
            return classScheduleGateway.GetClassSchedule(departmentId);
        }
 
    }
}