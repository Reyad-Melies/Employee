using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Employee.Data
{
    public class ApplicationContextDesignFactory : DesignTimeDbContextFactoryBase<EmployeeContext>
    {
        public ApplicationContextDesignFactory() : base("DefaultConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
        { }
        protected override EmployeeContext CreateNewInstance(DbContextOptions<EmployeeContext> options)
        {
            return new EmployeeContext(options);
        }
    }
}