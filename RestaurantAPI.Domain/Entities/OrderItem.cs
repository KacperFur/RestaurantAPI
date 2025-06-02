namespace RestaurantAPI.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid OrderItemId { get; set; } 
        public int? OrderId { get; set; } 
        public int? MenuItemId { get; set; } 
        public int Quantity { get; set; } 
        public decimal Price { get; set; } 
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public MenuItem? MenuItem { get; set; }
        public OrderItem(){ }
    }
}
