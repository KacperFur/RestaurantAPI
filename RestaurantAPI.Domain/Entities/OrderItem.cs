namespace RestaurantAPI.Entities
{
    public class OrderItem(int id, int orderId, int menuItemId, int quantity, decimal price)
    {
        public int Id { get; set; } = id;
        public int OrderId { get; set; } = orderId;
        public int MenuItemId { get; set; } = menuItemId;
        public int Quantity { get; set; } = quantity;
        public decimal Price { get; set; } = price;
        public decimal TotalPrice => Quantity * Price; 
        public Order Order { get; set; }
        public MenuItem Dish { get; set; }
    }
}
