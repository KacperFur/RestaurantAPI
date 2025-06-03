namespace RestaurantAPI.Application.Models
{
    public class UpdateMenuItemDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string MealType { get; set; }
    }
}
