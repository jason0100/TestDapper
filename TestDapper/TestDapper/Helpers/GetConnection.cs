using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDapper.Helpers;
using TestDapper.Models;

namespace TestDapper.Helpers
{
	public class GetConnection
	{
		private readonly IConfiguration _config;
				public GetConnection(IConfiguration config) {
			_config = config;
			//aes = aesHelper;
		}
		public  string applePenConnection()
		{
			var aes = new AESHelper(_config);
			string applePenConnection = null;
			var builder = new SqlConnectionStringBuilder(_config.GetConnectionString("applePenConnection"));
			var result = new ResultModel();
			result = aes.Decrypt(_config["DbPassword"]);
			if(result.r)
			builder.Password = result.d.ToString();
			applePenConnection = builder.ConnectionString;
			return applePenConnection;
		}

		public string manufactureConnection()
		{
			var aes = new AESHelper(_config);
			string applePenConnection = null;
			var builder = new SqlConnectionStringBuilder(_config.GetConnectionString("manufactureConnection"));
			var result = new ResultModel();
			result = aes.Decrypt(_config["DbPassword"]);
			if (result.r)
				builder.Password = result.d.ToString();
			applePenConnection = builder.ConnectionString;
			return applePenConnection;
		}
	}
}
