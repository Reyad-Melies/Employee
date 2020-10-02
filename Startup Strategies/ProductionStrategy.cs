using Microsoft.Extensions.Configuration;
using Employee.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Employee.StartupStrategies;

namespace EmployeesInformationManager.Startup_Strategies
{
    public class ProductionStrategy : AbstractStrategy
    {
        public ProductionStrategy(IConfiguration configuration)
            : base(configuration)
        {
        }

        override
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            services.AddDbContext<EmployeeContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("EmployeeContext");
                options.UseSqlServer(connectionString);
            });
        }

        override
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Emps/Error");
            app.UseHsts();
            ConfigureCommon(app);
        }
    }
}