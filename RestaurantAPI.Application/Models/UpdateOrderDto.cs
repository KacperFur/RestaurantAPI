namespace RestaurantAPI.Application.Models
{
    public class UpdateOrderDto
    {
        public int? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
