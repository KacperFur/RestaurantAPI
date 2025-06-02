using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Models
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public Guid OrderItemId { get; set; }
        public int? MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public MenuItemDto MenuItem { get; set; }
    }
}
