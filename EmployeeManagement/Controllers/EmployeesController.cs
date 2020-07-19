using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController()
        {
            this._context = new ApplicationDbContext();
        }
        // GET: Employees
        public ActionResult Index()
        {
            List<Employee> employees = _context.Employees.Include(x => x.Department).ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Create()
        {
            EmployeeForm viewModel = new EmployeeForm();
            viewModel.Departments = _context.Departments.ToList();
            return View("EmployeeForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Employee employee)
        {
            if(employee.Id == 0)
            {
                _context.Employees.Add(employee);
            }
            else
            {
                Employee employeeInDb = _context.Employees.Where(e => e.Id == employee.Id).FirstOrDefault();
                employeeInDb.Name = employee.Name;
                employeeInDb.Email = employee.Email;
                employeeInDb.DepartmentId = employee.DepartmentId;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Employee employee = _context.Employees.Where(e => e.Id == id).FirstOrDefault();
            EmployeeForm viewModel = new EmployeeForm()
            {
                Employee = employee,
                Departments = _context.Departments.ToList()
            };
            return View("EmployeeForm", viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Employee employee = _context.Employees.Where(e => e.Id == id).FirstOrDefault();
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}