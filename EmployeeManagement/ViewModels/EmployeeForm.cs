using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeForm
    {
        public Employee Employee { get; set; }
        public List<Department> Departments { get; set; }
    }
}