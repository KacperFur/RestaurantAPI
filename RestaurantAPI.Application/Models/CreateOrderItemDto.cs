namespace RestaurantAPI.Application.Models
{
    public class CreateOrderItemDto
    {
        public int? MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
