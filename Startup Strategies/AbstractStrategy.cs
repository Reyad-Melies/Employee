using Employee.Repository;
using Employee.Service;
using Employee.StartupStrategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.StartupStrategies
{
    public abstract class AbstractStrategy :  IStartupStrategy
    {

        protected IConfiguration Configuration { get; }
        public AbstractStrategy(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Configure(IApplicationBuilder app);

        protected void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddScoped<IEmpRepository, EmpRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();
            services.AddScoped<EmployeeService, EmployeeService>();
            services.AddScoped<VacationService, VacationService>();
            services.AddControllersWithViews();
        }

        protected void ConfigureCommon(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}