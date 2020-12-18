using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDapper.Helpers;
using TestDapper.Models;

namespace TestDapper.Data
{
	public class GetConnection
	{
		private readonly IConfiguration _config;
		private readonly IAESHelper _aesHelper;
		public GetConnection(IConfiguration config, IAESHelper aesHelper) {
			_config = config;
			_aesHelper = aesHelper;
		}
		public  string applePenConnection()
		{
			string applePenConnection = null;
			var builder = new SqlConnectionStringBuilder(_config.GetConnectionString("applePenConnection"));
			var result = new ResultModel();
			result = _aesHelper.Decrypt(_config["DbPassword"]);
			if(result.r)
			builder.Password = result.d.ToString();
			applePenConnection = builder.ConnectionString;
			return applePenConnection;
		}

		public string manufactureConnection()
		{
			string applePenConnection = null;
			var builder = new SqlConnectionStringBuilder(_config.GetConnectionString("manufactureConnection"));
			var result = new ResultModel();
			result = _aesHelper.Decrypt(_config["DbPassword"]);
			if (result.r)
				builder.Password = result.d.ToString();
			applePenConnection = builder.ConnectionString;
			return applePenConnection;
		}
	}
}
