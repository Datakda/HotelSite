using HotelSite.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSite.ViewModels
{
    [OrderValidationAttribute]
    public class OrderViewModel
    {

        [Required]
        public DateTime StartDate { set; get; }

        [Required]
        public DateTime EndDate { set; get; }

        public string UserName { set; get; }


    }
}
