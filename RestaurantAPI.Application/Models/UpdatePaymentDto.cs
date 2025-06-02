using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Models
{
    public class UpdatePaymentDto
    {
        public string Method { get; set; }      
        public string Status { get; set; }   
        public DateTime? PaidAt { get; set; }
    }
}
