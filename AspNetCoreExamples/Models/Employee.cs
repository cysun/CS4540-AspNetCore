using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreExamples.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Hash { get; set; }

        public bool IsAdmin { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateHired { get; set; }

        public int? SupervisorId { get; set; }
        public Employee Supervisor { get; set; }

        public Employee() { }

        public Employee(int id, string name, DateTime dateHired, int? supervisorId)
        {
            Id = id;
            Name = name;
            DateHired = dateHired;
            SupervisorId = supervisorId;
        }
    }
}
