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
