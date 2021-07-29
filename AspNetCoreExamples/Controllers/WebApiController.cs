using AspNetCoreExamples.Models;
using AspNetCoreExamples.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreExamples.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WebApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public WebApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("employees")]
        public List<Employee> GetEmployees()
        {
            return _employeeService.GetEmployees();
        }

        [HttpGet("employees/{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployee(id);
            if (employee != null)
                return Ok(employee);
            else
                return NotFound();
        }

        [HttpPut("employees/{employeeId}/supervisor/{supervisorId}")]
        public IActionResult SetSupervisor(int employeeId, int supervisorId)
        {
            var employee = _employeeService.GetEmployee(employeeId);
            if (employee == null)
                return NotFound();

            employee.SupervisorId = supervisorId;
            _employeeService.SaveChanges();

            return Ok();
        }
    }
}
