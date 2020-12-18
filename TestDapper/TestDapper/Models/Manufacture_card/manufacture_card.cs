using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestDapper.Models.Manufacture_card
{
    public class manufacture_card
    {
        public int id { get; set; }
        public int card_id { get; set; }
      
        [Required]
        public string user_uuid { get; set; }
        [Required]
        public string name { get; set; }
   
        public string name_en { get; set; }
        [Required]
        public string national_id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? birthdate { get; set; }
        public string birthplace { get; set; }
        [Required]
        public string address { get; set; }
        public int city_id { get; set; }
        public int district_id { get; set; }
        public int village_id { get; set; }

        //public string city { get; set; }
        //public string district { get; set; }
        //public string village { get; set; }
        [Required]
        public string military_service { get; set; }
        public string spouse_name { get; set; }
        [Required]
        public string father_name { get; set; }
        [Required]
        public string mother_name { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string landline { get; set; }
        [Required]
        //[Phone]
        public string mobile { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string photo { get; set; }
        public byte[] certificate_login { get; set; }
        public byte[] certificate_sign { get; set; }
        public string lot_number { get; set; }
        [Required]
        public int? status { get; set; }//0 = not send to manufacture (default),1 = sent to manufacture,2 = defective card
        public Byte? selected { get; set; }
        [Required]
        [Range(0, 1)]
        public int making { get; set; }
        public DateTime? send_pending { get; set; }
        public DateTime? send_to_DCA { get; set; }
        public DateTime? sent_to_household { get; set; }
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }
    }
}
