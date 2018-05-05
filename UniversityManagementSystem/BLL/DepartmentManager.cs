using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGateway=new DepartmentGateway();
        public string Save(Department aDepartment)
        {
            //if (!(IsDepartmentNameExits(aDepartment)))
            //{
            //    return "Department code must be between 2 to 7 character length";
            //}
            if (departmentGateway.IsExistName(aDepartment.Name))
            {
                return "Department Name Already Exists";
            }
            if (departmentGateway.IsEXistCode(aDepartment.Code))
            {
                return "Department Code Already Exists. Department Code must be uinque";
            }
            if (departmentGateway.Insert(aDepartment) > 0)
            {
                return "Saved Successfully";
            }
            return "Failed to save";
        }

        public IEnumerable<Department> GetDepartments()
        {
            return departmentGateway.GetDepartments();
        }

    }
}