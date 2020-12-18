using TestDapper.Models.Manufacture_card;
using Microsoft.EntityFrameworkCore;

namespace TestDapper.Data
{
    public class manufacture_Context : DbContext
    {
        public manufacture_Context(DbContextOptions<manufacture_Context> options) : base(options)
        {
        }
        
        public DbSet<manufacture_card> card { get; set; }
      
    }
}