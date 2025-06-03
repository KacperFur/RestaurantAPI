namespace RestaurantAPI.Application.Models
{
    public class CreateOrderDto
    {
        public int? UserId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
