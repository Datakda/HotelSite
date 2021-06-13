using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSite.Models
{
    [NotMapped]
    public class Order
    {
        public int? Id { set; get; }

        public DateTime StartDate { set; get; }

        public DateTime EndDate { set; get; }

        public string? UserName { set; get; }



    }
}
