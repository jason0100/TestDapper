using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestDapper.Models.Manufacture_card
{
    public class manufacture_card_caForQuery
    {
        [Required]
        [Range(0,1)]
        public int? signature { get; set; }
        [Required]
        public string lot_number { get; set; }
       

    }
}
