using AspNetCoreExamples.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreExamples.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // We use Name as username in login, so Name should be unique.
            modelBuilder.Entity<Employee>().HasIndex(e => e.Name).IsUnique();
        }
    }
}
