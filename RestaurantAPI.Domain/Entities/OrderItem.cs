namespace RestaurantAPI.Entities
{
    public class OrderItem(int id, int orderId, int menuItemId, int quantity, decimal price)
    {
        public int Id { get; set; } = id;
        public Guid OrderItemId { get; set; } = Guid.NewGuid();
        public int? OrderId { get; set; } = orderId;
        public int? MenuItemId { get; set; } = menuItemId;
        public int Quantity { get; set; } = quantity;
        public decimal Price { get; set; } = price;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
