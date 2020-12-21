using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using System.Net;

namespace TestDapper.Middleware
{
	public class MyExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public MyExceptionMiddleware(RequestDelegate next)
		{
			_next = next;

		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleException(context, ex);
			}
		}
		private static Task HandleException(HttpContext context, Exception ex)
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";
			var message = ex.Message;
			// log 
			logger.Error(ex, $"Ex massage: {message}, StackTrace: {ex.StackTrace}", ex);
			var result = JsonConvert.SerializeObject(new
			{ Timestamp = DateTimeOffset.UtcNow, Message = message, Result = "System error" });
			return context.Response.WriteAsync(result);
		}
	}
}
