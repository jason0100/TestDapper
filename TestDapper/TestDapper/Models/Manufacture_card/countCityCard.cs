using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestDapper.Models.Manufacture_card
{
    public class countCityCard
    {
        public int city_id { get; set; }
        /// <summary>
        /// 0=not send to manufacture, 1=sent to manufacture, 2 = defective card, 3 = interrupt
        /// </summary>
        public int? status { get; set; }
    }
}
