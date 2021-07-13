using AspNetCoreExamples.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreExamples.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();

        Employee GetEmployee(int id);

        void AddEmployee(Employee employee);

        void SaveChanges();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _db;

        public EmployeeService(AppDbContext db)
        {
            _db = db;
        }

        public List<Employee> GetEmployees()
        {
            return _db.Employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _db.Employees.Where(e => e.Id == id).Include(e => e.Supervisor).SingleOrDefault();
        }

        public void AddEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
        }

        public void SaveChanges() => _db.SaveChanges();
    }

    public class MockEmployeeService : IEmployeeService
    {
        private List<Employee> employees;

        public MockEmployeeService()
        {
            employees = new List<Employee> {
                new Employee(1, "John", new DateTime(2015, 1, 10), null),
                new Employee(2, "Jane", new DateTime(2015, 2, 20), 1),
                new Employee(3, "Tom", new DateTime(2016, 6, 19), 2),
                new Employee(4, "Bob", new DateTime(2016, 6, 20), 2) };

            employees[1].Supervisor = employees[0];
            employees[2].Supervisor = employees[1];
            employees[3].Supervisor = employees[1];
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee GetEmployee(int id)
        {
            return employees[id - 1];
        }

        public void AddEmployee(Employee employee)
        {
            employee.Id = employees.Count;
            employees.Add(employee);
        }

        public void SaveChanges() { }
    }
}
