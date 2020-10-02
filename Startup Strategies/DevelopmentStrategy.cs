using Microsoft.Extensions.Configuration;
using Employee.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace Employee.StartupStrategies
{
    public class DevelopmentStrategy : AbstractStrategy
    {
        public DevelopmentStrategy(IConfiguration configuration)
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
            app.UseDeveloperExceptionPage();
            ConfigureCommon(app);
        }
    }
}