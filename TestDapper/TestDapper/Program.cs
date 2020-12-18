using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestDapper.Data;

namespace TestDapper
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var context = services.GetRequiredService<MyContext>();
					DatabaseInitializer.Initialize(context);

					var context2 = services.GetRequiredService<manufacture_Context>();
					DatabaseInitializer.Initialize(context2);
				}
				catch (Exception e)
				{
					/*var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(e, "An error occurred while seeding the database.");*/
				}
			}
			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((hostContext, config) =>
			{
				var env = hostContext.HostingEnvironment;
				config.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
					 .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
				.AddJsonFile(path: $"Configuration/settings.json", optional: false, reloadOnChange: true);

			})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
