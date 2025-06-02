using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Models
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public Guid PaymentId { get; set; }
        public int? OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
