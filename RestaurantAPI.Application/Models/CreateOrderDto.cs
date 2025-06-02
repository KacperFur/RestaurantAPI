using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Models
{
    public class CreateOrderDto
    {
        public int? UserId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
