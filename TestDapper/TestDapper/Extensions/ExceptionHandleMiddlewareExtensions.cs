using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using TestDapper.Middleware;
using NLog;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace TestDapper.Extensions
{
    public static class ExceptionHandleMiddlewareExtensions
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
		public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<MyExceptionMiddleware>();
		}



	}
}
