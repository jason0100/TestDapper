using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDapper.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using TestDapper.Helpers;
using TestDapper.Extensions;

namespace TestDapper
{
	public class Startup
	{
		private IConfiguration _config { get; }
		//private GetConnection _conn;

		public Startup(IConfiguration config
			)
		{
			_config = config;

		}



		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
				var conn = new GetConnection(_config);
						
				services.AddControllers()
				.AddNewtonsoftJson(options =>
					  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
						);

				services.AddDbContext<MyContext>(options =>
					options.UseSqlServer(conn.applePenConnection()));
				//options.UseSqlServer(_config.GetConnectionString("applePenConnection")));
				

				services.AddDbContext<manufacture_Context>(options =>
				options.UseSqlServer(conn.manufactureConnection()));
				//options.UseSqlServer(_config.GetConnectionString("manufactureConnection")));

			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//app.UseExceptionHandler(new ExceptionHandlerOptions());
			app.UseExceptionHandleMiddleware();
			
			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			
		}
	}
}
