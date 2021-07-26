using AspNetCoreExamples.Models;
using AspNetCoreExamples.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreExamples.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthorizationService _authorizationService;

        public EmployeesController(IEmployeeService employeeService, IAuthorizationService authorizationService)
        {
            _employeeService = employeeService;
            _authorizationService = authorizationService;
        }

        public IActionResult Index()
        {
            return View(_employeeService.GetEmployees());
        }

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
                return Forbid();

            return View(_employeeService.GetEmployee(id));
        }

        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public IActionResult Add(Employee employee)
        {
            employee.Hash = BCrypt.Net.BCrypt.HashPassword("abcd");
            _employeeService.AddEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
                return Forbid();

            ViewBag.Supervisors = _employeeService.GetEmployees()
                .Where(e => e.Id != id)
                .Select(e => new SelectListItem(e.Name, e.Id.ToString()))
                .ToList();
            return View(_employeeService.GetEmployee(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, Employee update)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
                return Forbid();

            var employee = _employeeService.GetEmployee(id);
            employee.Name = update.Name;
            employee.DateHired = update.DateHired;
            employee.SupervisorId = update.SupervisorId;
            _employeeService.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Two(int id1, int id2)
        {
            var employee1 = _employeeService.GetEmployee(id1);
            var employee2 = _employeeService.GetEmployee(id2);
            return View((employee1, employee2));
        }
    }
}
