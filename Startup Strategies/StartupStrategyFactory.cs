using Employee.StartupStrategies;
using EmployeesInformationManager.Startup_Strategies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Startup_Strategies
{
    public class StartupStrategyFactory
    {
		private IConfiguration configuration;

		public StartupStrategyFactory(IConfiguration _configuration)
		{
			configuration = _configuration;
		}

		public IStartupStrategy CreateStartupStrategy(IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				return new DevelopmentStrategy(configuration);
			return new ProductionStrategy(configuration);
		}
	}
}