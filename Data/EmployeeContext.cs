using Employee.Models;
using Microsoft.EntityFrameworkCore;
namespace Employee.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
           : base(options)
        {
        }
        public DbSet<Emp> Emp { get; set; }
        public DbSet<Vacation> Vacation { get; set; }

    }
}
