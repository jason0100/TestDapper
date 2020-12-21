using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDapper.Helpers;
using TestDapper.Models;

namespace TestDapper.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AESController : Controller
	{
		private readonly IConfiguration _config;
		public AESController(IConfiguration config) {
			_config = config;
		}
		[HttpGet]
		public ResultModel Encrypt(string text)
		{
			var aes = new AESHelper(_config);

			var result = new ResultModel();
			result.r = true;

			var cipherText = aes.Encrypt(text);
						result.m = cipherText;
			return result;
		}
	}
}
