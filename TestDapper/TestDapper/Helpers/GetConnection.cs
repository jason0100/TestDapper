using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
			string fileName = "c:\\TestDapper.json";
			var myJsonString = File.ReadAllText(fileName);
			var myJObject = JObject.Parse(myJsonString);
			var pw=myJObject.SelectToken("DbPassword").Value<string>();

			var aes = new AESHelper(_config);
			string applePenConnection = null;
			var builder = new SqlConnectionStringBuilder(_config.GetConnectionString("applePenConnection"));
			var result = new ResultModel();
			//result = aes.Decrypt(_config["DbPassword"]);
			result = aes.Decrypt(pw);
			if (result.r)
			builder.Password = result.d.ToString();
			applePenConnection = builder.ConnectionString;
			return applePenConnection;
		}

		public string manufactureConnection()
		{
			string fileName = "c:\\TestDapper.json";
			var myJsonString = File.ReadAllText(fileName);
			var myJObject = JObject.Parse(myJsonString);
			var pw = myJObject.SelectToken("DbPassword").Value<string>();

			var aes = new AESHelper(_config);
			string applePenConnection = null;
			var builder = new SqlConnectionStringBuilder(_config.GetConnectionString("manufactureConnection"));
			var result = new ResultModel();
			//result = aes.Decrypt(_config["DbPassword"]);
			result = aes.Decrypt(pw);
			if (result.r)
				builder.Password = result.d.ToString();
			applePenConnection = builder.ConnectionString;
			return applePenConnection;
		}
	}
}
