using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestDapper.Models.applepen
{
    public class applePenModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int appleId{get;set;}
        public apple apple { get; set; }

    }

    public class apple{
        public int id { get; set; }
        public string name { get; set; }
    }

    public class appleForEdit
    {
        [Required]
        public int id { get; set; }
        public string name { get; set; }
    }
}
