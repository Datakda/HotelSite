using HotelSite.Models;
using HotelSite.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSite.Validation
{
    public class OrderValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            OrderViewModel order = value as OrderViewModel;
            if (order.StartDate > order.EndDate)
            {
                this.ErrorMessage = "Неверная дата";
                return false;
            }
            return true;
        }
    }
}
