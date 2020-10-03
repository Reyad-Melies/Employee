using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.StartupStrategies
{
    public interface IStartupStrategy
    {
        public void ConfigureServices(IServiceCollection services);

        public void Configure(IApplicationBuilder app);
    }
}
