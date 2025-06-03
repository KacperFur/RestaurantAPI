namespace RestaurantAPI.Application.Models
{
    public class CreateMenuItemDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public string MealType { get; set; }
    }
}
