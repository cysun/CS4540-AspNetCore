using AspNetCoreExamples.Models;
using AspNetCoreExamples.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreExamples.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View(_employeeService.GetEmployees());
        }

        public IActionResult Details(int id)
        {
            return View(_employeeService.GetEmployee(id));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            _employeeService.AddEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Supervisors = _employeeService.GetEmployees()
                .Where(e => e.Id != id)
                .Select(e => new SelectListItem(e.Name, e.Id.ToString()))
                .ToList();
            return View(_employeeService.GetEmployee(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Employee update)
        {
            var employee = _employeeService.GetEmployee(id);
            employee.Name = update.Name;
            employee.DateHired = update.DateHired;
            employee.SupervisorId = update.SupervisorId;
            _employeeService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
