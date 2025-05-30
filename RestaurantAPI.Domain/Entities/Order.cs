using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Order(int id, int userId, int dishId, DateTime orderDate, OrderStatus status, decimal totalAmount, List<OrderItem> orderItems)
    {
        public int Id { get; set; } = id;
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public int? UserId { get; set; } = userId;
        public DateTime OrderDate { get; set; } = orderDate;
        public string Status { get; set; } = status.ToString();
        public decimal TotalAmount { get; set; } = totalAmount;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
