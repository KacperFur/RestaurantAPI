using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Order(int id, int userId, int dishId, DateTime orderDate, OrderStatus status, decimal totalAmount, List<OrderItem> orderItems)
    {
        public int Id { get; set; } = id;
        public int UserId { get; set; } = userId;
        public int DishId { get; set; } = dishId;
        public DateTime OrderDate { get; set; } = orderDate;
        public OrderStatus Status { get; set; } = status;
        public decimal TotalAmount { get; set; } = totalAmount;
        public List<OrderItem> OrderItems { get; set; } = orderItems;
    }
}
