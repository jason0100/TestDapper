using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDapper.Data;

namespace TestDapper.Data
{
	public class DatabaseInitializer
	{
		public static void Initialize(DbContext context)
		{
			context.Database.EnsureCreated();


		}
	}
}
