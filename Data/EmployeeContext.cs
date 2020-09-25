using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Employee.Models;
namespace Employee.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
           : base(options)
        {
        }
        public DbSet<Emp> Emp { get; set; }
        public DbSet<VacationSchedule> VacationSchedules { get; set; }
        public DbSet<VacationCasual> VacationCasuals { get; set; }
        public DbSet<Employee.Models.All> All { get; set; }

    }
}
