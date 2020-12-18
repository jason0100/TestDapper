using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TestDapper.Data;
using TestDapper.Models;
using TestDapper.Models.Manufacture_card;
#pragma warning disable 1591
namespace TestDapper.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class Manufacture_cardController : ControllerBase
	{
		private readonly IConfiguration _config;
		//private readonly services_Context _services_Context;
		//private readonly server_Context _server_Context;
		//private readonly household_Context _household_context;
		private readonly manufacture_Context _manufacture_context;
		//private readonly share_Context _share_context;
		//private readonly IUploadHelper _uploadHelper;

		public Manufacture_cardController(
			IConfiguration config
			//, services_Context services_Context
			//, server_Context server_Context
			//, household_Context household_context
			, manufacture_Context manufacture_context
		 //, share_Context share_context
		 //, IUploadHelper uploadHelper
		 )
		{
			_config = config;
			//_services_Context = services_Context;
			//_server_Context = server_Context;
			//_household_context = household_context;
			_manufacture_context = manufacture_context;
			//_share_context = share_context;
			//_uploadHelper = uploadHelper;
		}

		/// <summary>
		/// 得到製卡檔/證件簽章/憑證簽章資料
		/// </summary>
		/// <remarks>
		///     Sample request:
		///
		///     GET /manufacture_card?ot_number=202004240001&amp;page_number=1&amp;page_sizes=5
		///
		/// </remarks>
		/// <returns>ResultModel</returns>
		[Produces("application/json")]
		[HttpGet]
		public async Task<ResultModel> GetManufactureFile()
		{
			var result = new ResultModel();
			var sw = new Stopwatch();
			sw.Start();
			/*	using (var conn = new SqlConnection(_config.GetConnectionString("manufactureConnection")))
				{
					string strSql = "SELECT  * FROM card";
					var existCards = conn.Query<manufacture_card>(strSql).AsEnumerable().FirstOrDefault();
					result.r = true;
					result.d = existCards;

				}*/
			var existCards = _manufacture_context.card.Find(1);
			result.r = true;
			result.d = existCards;
			sw.Stop();
			result.m = "Elapsed time=" + sw.ElapsedMilliseconds / 1000.0 + "s";

			return result;
		}

		///// <summary>
		///// 證件/憑證簽署,回傳整包lot的cards資訊
		///// </summary>
		///// <remarks>
		///// Sample request:
		/////
		/////     GET /manufacture_card?lot_number=123456&amp;signature="xxxxxx"
		/////
		///// </remarks>
		///// <returns></returns>
		//[Produces("application/json")]
		//[HttpGet("ca")]
		//public async Task<ResultModel> SendFactoryFileToCa([FromQuery]manufacture_card_caForQuery model)
		//{
		//    var result = new ResultModel();
		//    var query = _manufacture_context.card
		//        .Where(a => (string.IsNullOrEmpty(model.lot_number)) ? true : a.lot_number == model.lot_number)

		//        .ToList();

		//    result = ErrorCode.SUCCESS(query, null, "cht");
		//    return result;
		//}

		///// <summary>
		///// 製卡檔發送,Note:回傳整包lot的cards資訊
		///// </summary>
		///// <param name="model"></param>
		///// <returns></returns>
		////public async Task<ResultModel> SendFactoryFiles([FromQuery]manufacture_card_caForQuery model)
		////{
		////    var result = new ResultModel();

		////    return result;
		////}



		[HttpPost("card")]
		public async Task<ResultModel> ToFactoryFile()
		{
			var result = new ResultModel();

			var sw = new Stopwatch();


			sw.Start();
			using (var conn = new SqlConnection(_config.GetConnectionString("manufactureConnection")))
			{
				try
				{
					//string strSql = "SELECT TOP(10) * FROM card";
					//var existCards = conn.Query<manufacture_card>(strSql).ToList();
					var sql = @"INSERT INTO card(card_id,user_uuid,name,name_en,national_id,birthdate,birthplace,address,city_id,district_id,village_id)
                            VALUES (@card_id,@user_uuid,@name,@name_en,@national_id,@birthdate,@birthplace,@address,@city_id,@district_id,@village_id); ";
					var pList = new List<DynamicParameters>();//多筆資料
					for (int i = 0; i < 10000; i++)
					{
						var p = new DynamicParameters();
						p.Add("@card_id", 1, DbType.Int32, ParameterDirection.Input);
						p.Add("@user_uuid", "uuid", DbType.String, ParameterDirection.Input);
						p.Add("@name", "name", DbType.String, ParameterDirection.Input);
						p.Add("@name_en", "english name", DbType.String, ParameterDirection.Input);
						p.Add("@national_id", "A123456789", DbType.String, ParameterDirection.Input);
						p.Add("@birthdate", "2020/01/01", DbType.DateTime, ParameterDirection.Input);
						p.Add("@birthplace", "birthplace", DbType.String, ParameterDirection.Input);
						p.Add("@address", "address", DbType.String, ParameterDirection.Input);
						p.Add("@city_id", 1, DbType.Int32, ParameterDirection.Input);
						p.Add("@district_id", 1, DbType.Int32, ParameterDirection.Input);
						p.Add("@village_id", 1, DbType.Int32, ParameterDirection.Input);
						pList.Add(p);
					}

					conn.Execute(sql, pList);

					//  var sql = "INSERT INTO card (card_id,  user_uuid, name, birthdate, address,national_id" +
					//      ", military_service, spouse_name, father_name, mother_name, birthplace" +
					//      ", gender, landline, mobile, email, name_en, photo, created, status, making, send_pending, sent_to_household)" +
					//      " VALUES(@card_id,  @user_uuid, @name, @birthdate, @address, @national_id" +
					//      ", @military_service, @spouse_name, @father_name, @mother_name, @birthplace" +
					//      ", @gender, @landline, @mobile, @email, @name_en, @photo, @created, @status, @making, @send_pending, @sent_to_household);";

					//await  db.ExecuteAsync(sql, listData);

					//await _manufacture_context.SaveChangesAsync();

					//Debug.WriteLine("Batch done=" + swBatch.ElapsedMilliseconds / 1000 + "s");




				}
				catch (Exception e)
				{
					result.r = false;
					result.d = e.Message.ToString();
					return result;

				}


			}
			sw.Stop();
			result.r = true;
			Debug.WriteLine("All done=" + sw.ElapsedMilliseconds / 1000 + "s");
			result.d = ("All done=" + sw.ElapsedMilliseconds / 1000.0 + "s");
			return result;

		}
		[HttpPost("card2")]
		public async Task<ResultModel> ToFactoryFile2()
		{
			var result = new ResultModel();

			var sw = new Stopwatch();


			sw.Start();
			using (var conn = new SqlConnection(_config.GetConnectionString("manufactureConnection")))
			{
				try
				{
					for (int i = 0; i < 10000; i++)
					{
						var card = new manufacture_card();
						card.name = "name";
						card.name_en = "engllish name";
						card.user_uuid = "uuid";
						card.national_id = "A123456789";
						card.birthdate = Convert.ToDateTime("2020/01/01");
						card.birthplace = "birthplace";
						card.address = "address";
						card.city_id = 1;
						card.district_id = 1;
						card.village_id = 1;
						await _manufacture_context.AddAsync(card);
					}
					await _manufacture_context.SaveChangesAsync();



				}
				catch (Exception e)
				{
					result.r = false;
					result.d = e.InnerException.Message.ToString();
					return result;

				}


			}
			sw.Stop();
			result.r = true;
			Debug.WriteLine("All done=" + sw.ElapsedMilliseconds / 1000 + "s");
			result.d = ("All done=" + sw.ElapsedMilliseconds / 1000.0 + "s");
			return result;

		}
	}
}