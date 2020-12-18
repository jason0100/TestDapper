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

namespace TestDapper
{
    public class Startup
    {
        
        private readonly GetConnection _conn;
        public Startup(IConfiguration configuration, GetConnection conn)
        {
            Configuration = configuration;
            _conn = conn;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
		/*	var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("applePenConnection"));
			builder.Password = Configuration["TestDapper:DbPassword"];
			applePenConnection = builder.ConnectionString;*/

			services.AddControllers()
                 .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

     
            services.AddDbContext<MyContext>(options =>
					options.UseSqlServer(_conn.applePenConnection()));

			services.AddDbContext<manufacture_Context>(options =>
                   options.UseSqlServer(_conn.manufactureConnection()));

            services.AddTransient<GetConnection>();
			services.AddScoped<IAESHelper, AESHelper>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
