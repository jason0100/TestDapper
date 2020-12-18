using TestDapper.Models;
using TestDapper.Models.applepen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDapper.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options) { }

        
        public DbSet<applePenModel> applePens { get; set; }
        public DbSet<apple> apples { get; set; }

    }
}
