using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Employee.Data;
using Microsoft.EntityFrameworkCore;
using Employee.Repository;
using Employee.Service;
using Employee.StartupStrategies;
using Employee.Startup_Strategies;

namespace Employee
{
    /* public class Startup
     {
         public Startup(IConfiguration configuration)
         {
             Configuration = configuration;
         }

         public IConfiguration Configuration { get; }

         // This method gets called by the runtime. Use this method to add services to the container.
         public void ConfigureServices(IServiceCollection services)
         {

             services.AddScoped<IEmpRepository, EmpRepository>();
             services.AddScoped<IVacationRepository, VacationRepository>();
             services.AddScoped<EmployeeService, EmployeeService>();
             services.AddScoped<VacationService, VacationService>();
             services.AddControllersWithViews();
             services.AddDbContext<EmployeeContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("EmployeeContext")));

         }

         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
         {
             if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }
             else
             {
                 app.UseExceptionHandler("/Home/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                 app.UseHsts();
             }
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
 }*/




    public class Startup
    {
        private IStartupStrategy startupStrategy { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            StartupStrategyFactory startupStrategyFactory = new StartupStrategyFactory(configuration);
            startupStrategy = startupStrategyFactory.CreateStartupStrategy(env);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            startupStrategy.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            startupStrategy.Configure(app);
        }
    }
}