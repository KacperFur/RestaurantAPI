namespace RestaurantAPI.Application.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
