using Employee.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
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