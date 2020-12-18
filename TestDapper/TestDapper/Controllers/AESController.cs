using Microsoft.AspNetCore.Mvc;
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
		private readonly IAESHelper _aesHelper;
		public AESController(IAESHelper aesHelper) {
			_aesHelper = aesHelper;
		}
		[HttpGet]
		public ResultModel Encrypt(string text)
		{
			text = "t7689n";
			var result = new ResultModel();
			result.r = true;

			var cipherText = _aesHelper.Encrypt(text);
						result.m = cipherText;
			return result;
		}
	}
}
