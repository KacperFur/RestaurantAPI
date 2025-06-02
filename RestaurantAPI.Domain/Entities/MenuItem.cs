using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public Guid MenuItemId { get; set; } 
        public string Name { get; set; } 
        public string? Description { get; set; } 
        public decimal Price { get; set; } 
        public int? CategoryId { get; set; } 
        public string MealType { get; set; } 
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public MenuItem(){}
    }
}
