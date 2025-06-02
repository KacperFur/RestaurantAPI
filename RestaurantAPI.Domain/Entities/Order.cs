using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int? UserId { get; set; }
        public DateTime OrderDate { get; set; } 
        public string Status { get; set; } 
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Order(){}
    }
}
