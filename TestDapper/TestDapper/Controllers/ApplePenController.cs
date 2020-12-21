using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TestDapper.Data;
using TestDapper.Models.applepen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDapper.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using TestDapper.Models.Manufacture_card;
using System.Diagnostics;
using System.Data;
using TestDapper.Helpers;

namespace TestDapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplePenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MyContext _DBContext;
       

		public ApplePenController(MyContext DBContext, IConfiguration config
			//, IGetConnection conn
			)
        {
            _DBContext = DBContext;
            _config = config;
			//_conn = conn;
		}


        [HttpGet]
        public ResultModel Get(int id)
        {
            var sw = new Stopwatch();
            sw.Start();
			var result = new ResultModel();
			//var query = _DBContext.applePens
			//	.Include(a => a.apple)
			////.Select(a=>new { a,a.apple})
			//.ToList();
			var _conn = new GetConnection(_config);
            //using (var conn = new SqlConnection(_config.GetConnectionString("applePenConnection")))
            using (var conn = new SqlConnection(new GetConnection(_config).applePenConnection()))
				{
					conn.Open();
					//string strSql = "SELECT TOP(10) * FROM applePens";
					var sql = @"DECLARE @Id INT=1

						SELECT * 
						FROM applePens o 
						JOIN apples c
						ON  O.appleId=C.id
						WHERE o.id=@Id";
					var p = new DynamicParameters();
					p.Add("@Id", 1);
					//var query = conn.Query<applePenModel, apple, applePenModel>(sql, (o, c) => { o.apple = c; return o; }).ToList();
					//Query 方法的部分，我們必須按照我們 SELECT 欄位的順序告訴 Dapper 說依序對應的類別是哪些？
					//以及最後要回傳的類別是哪一個？依這個例子，我們 SELECT 出來的欄位分別依序是對應到 applePenModel、apple 類別，
					//最後要回傳的類別是 applePenModel。

					//var query = _DBContext.apples.AsNoTracking().ToList();
					var sql2 = @"select * from apples where id<@id";
					var query = conn.Query<applePenModel>(sql2,new { @id=100});
					//var query = conn.Query<apple>(sql);

					result.r = true;
					result.d = query;
				}
            //result.r = true;
            //result.d = query;
            
			sw.Stop();
            result.m = "Elapsed time =" + sw.ElapsedMilliseconds / 1000.0 + "s";

            return result;


        }


        [HttpPost]
        public ResultModel Create(apple model)
        {
            var _conn = new GetConnection(_config);
            var result = new ResultModel();
            using (var conn = new SqlConnection(new GetConnection(_config).applePenConnection()))
            {
                //INSERT DATA
                var sql = @"INSERT INTO apples(name)
                            VALUES (@name); ";
                var p = new DynamicParameters();
                p.Add("@name", model.name, DbType.String,direction:ParameterDirection.Input);
                p.Add("@r",  DbType.Int32, direction: ParameterDirection.Output);
                var sw = new Stopwatch();
                sw.Start();
                try
                {
                    //conn.Execute(sql, p);
                    var rr =conn.Execute("sp_create_apple", p, commandType: CommandType.StoredProcedure);
                    result.d = p.Get<dynamic>("@r");//接收SP回傳值
                }
                catch (Exception e)
                {
                    result.r = false;
                    result.d = e;
                    return result;
                }

                //GET latest data
                sql = @"SELECT TOP(1) * FROM apples ORDER BY id DESC";
                var query = conn.Query(sql).SingleOrDefault();
                sw.Stop();
                result.r = true;
                result.m = "Elapsed time = " + sw.ElapsedMilliseconds / 1000.0 + "s";
                //result.d = query;
            }
            return result;
        }

        [HttpPost("CreateBatch")]
        public ResultModel CreateBatch(apple model)
        {
            var result = new ResultModel();
            var _conn = new GetConnection(_config);
            using (var conn = new SqlConnection(new GetConnection(_config).applePenConnection()))
            {
                //INSERT DATA
                var sql = @"INSERT INTO apples(name)
                            VALUES (@name); ";
                var pList = new List<DynamicParameters>();//多筆資料
                for (int i = 0; i < 10000; i++)
                {
                    var p = new DynamicParameters();
                    p.Add("@name", model.name, DbType.String, direction: ParameterDirection.Input);
                    p.Add("@r", DbType.Int32, direction: ParameterDirection.ReturnValue);
                    pList.Add(p);
                }
               
                var sw = new Stopwatch();
                sw.Start();
                try
                {
                    conn.Execute(sql, pList);
                    //var rr = conn.Execute("sp_create_apple", pList, commandType: CommandType.StoredProcedure);
                    //result.d = p.Get<dynamic>("@r");//接收SP回傳值
                }
                catch (Exception e)
                {
                    result.r = false;
                    result.d = e;
                    return result;
                }

                //GET latest data
                sql = @"SELECT TOP(1) * FROM apples ORDER BY id DESC";
                var query = conn.Query(sql).SingleOrDefault();
                sw.Stop();
                result.r = true;
                result.m = "Elapsed time = " + sw.ElapsedMilliseconds / 1000.0 + "s";
                //result.d = query;
            }
            return result;
        }


        [HttpPut]
        public ResultModel Edit(appleForEdit model)
        {
            var result = new ResultModel();

            var existData = _DBContext.apples.FirstOrDefault(a => a.id == model.id);
            if (existData == null)
            {
                result.r = false;
                result.m = "Data not exist.";
                return result;
            }
            var _conn = new GetConnection(_config);
            using (var conn = new SqlConnection(new GetConnection(_config).applePenConnection()))
            {

                var sql = @"UPDATE apples SET name=@name WHERE id=@id";
                var p = new DynamicParameters();
                p.Add("@id", model.id);
                p.Add("@name", model.name);
                var sw = new Stopwatch();
                sw.Start();
                try
                {
                    conn.Execute(sql, p);

                }
                catch (Exception e)
                {
                    result.r = false;
                    result.d = e;
                    return result;
                }
               
                result.r = true;
                result.m = "Elapsed time = " + sw.ElapsedMilliseconds / 1000.0 + "s";
                
            }
            return result;
        }


        [HttpDelete("{id}")]
        public ResultModel Delete(int? id)
        {
            var result = new ResultModel();
            var existData = _DBContext.apples.FirstOrDefault(a => a.id == id);
            if (existData == null)
            {
                result.r = false;
                result.m = "Data not exist.";
                return result;
            }
            var _conn = new GetConnection(_config);
            using (var conn = new SqlConnection(new GetConnection(_config).applePenConnection()))
            {

                var sql = @"DELETE apples  WHERE id=@id";
                var p = new DynamicParameters();
                p.Add("@id", id);
              
                var sw = new Stopwatch();
                sw.Start();
                try
                {
                    conn.Execute(sql, p);

                }
                catch (Exception e)
                {
                    result.r = false;
                    result.d = e;
                    return result;
                }

                result.r = true;
                result.m = "Elapsed time = " + sw.ElapsedMilliseconds / 1000.0 + "s";

            }
            return result;
        }
        [HttpGet("Test")]
        public async Task<ResultModel> TestParameter() {
            var result = new ResultModel();
            var p = new DynamicParameters();
            
            
            p.Add("@id", 2, DbType.Int32, direction: ParameterDirection.Input);
            p.Add("@name", DbType.String, direction: ParameterDirection.Output);

            using (var conn = new SqlConnection(new GetConnection(_config).applePenConnection()))
            {

                var rr = conn.Execute("取得姓名", p, commandType: CommandType.StoredProcedure);
				result.d =p.Get<dynamic>("@name");//接收SP回傳值

            }
            result.r = true;
                return result;
        }

        [HttpGet("Test2")]
        public async Task<DataTable> Test2()
        {
            var result = new ResultModel();
            

            using (var conn = new SqlConnection(new GetConnection(_config).applePenConnection()))
            {

                var dr = conn.ExecuteReader("select * from apples where id=2");
                var dt = new DataTable();
                dt.Load(dr);
                return dt;

            }

            //return result;
            
        }

        public SqlConnection GetConnection() {
            var a = new SqlConnection(_config.GetConnectionString("applePenConnection"));
            return a;
        }
    }
}