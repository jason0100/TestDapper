using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace TestDapper.Models.Manufacture_card
{
    public class Manufacture_cardForQuery
    {
       
        [Range(0,1)]
        public int? signature { get; set; }
        public int? signed { get; set; }
 
        //[Required]
        public string lot_number { get; set; }
        //public DateTime? birthdate { get; set; }
        //public string birthplace { get; set; }

        //public string address { get; set; }
        //public string city { get; set; }
        //public string district { get; set; }
        //public string village { get; set; }
        //public string mobile { get; set; }


        //[EmailAddress]
        //public string email { get; set; }

        /// <summary>
        /// 資料庫有兩個欄位
        ///已領取卡片 Card mgmt. Collected cards status list
        ///certificate_login 如果他有值代表document sigature簽名了certificate_sign有值就代表certificate簽了
        ///signature:0 = document, 1 =certificate, signed:0 = not signed 1 = signed
        /// </summary>
        public int? status { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_number { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_sizes { get; set; }
        public string arrange_for { get; set; }
        public string arrange { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime? password_expiration_date_end { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime? password_expiration_date_init { get; set; }

    }
}
