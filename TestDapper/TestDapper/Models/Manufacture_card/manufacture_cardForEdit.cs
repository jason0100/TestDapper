using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace TestDapper.Models.Manufacture_card
{
    public class manufacture_cardForEdit
    {
        [Required]
        public string national_id { get; set; }
        
        [Range(0, 2)]
        public int? status { get; set; }//0 = not send to manufacture (default),1 = sent to manufacture,2 = defective card

        
        [Range(0,1)]
        public int? making { get; set; }//default 0 pend-ing ,1=making
        [DataType(DataType.Date)]
        public DateTime? send_pending { get; set; }
        [DataType(DataType.Date)]
        public DateTime? sent_to_household { get; set; }
        //[Required]
        public string lot_number { get; set; }
        public byte[] certificate_login { get; set; }
        public byte[] certificate_sign { get; set; }
        public Byte? selected { get; set; }

    }
}
