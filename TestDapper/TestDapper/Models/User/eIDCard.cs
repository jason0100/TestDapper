using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestDapper.Models.User
{
    public class eIDCard
    {
        [Key]
        public int id { get; set; }
        public string national_id { get; set; }
        public DateTime? household_received { get; set; }
        public DateTime? user_received { get; set; }
        public DateTime? user_activated { get; set; }
        public string? activation_location { get; set; }
        public int? status { get; set; }
        public DateTime? created { get; set; }
        public DateTime ?updated { get; set; }
    }
}
