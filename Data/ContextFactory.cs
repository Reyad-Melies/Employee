using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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