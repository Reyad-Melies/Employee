using Employee.Data;
using Employee.StartupStrategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
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