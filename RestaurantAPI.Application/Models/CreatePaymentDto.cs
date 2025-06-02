using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Models
{
    public class CreatePaymentDto
    {
        public int? OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
