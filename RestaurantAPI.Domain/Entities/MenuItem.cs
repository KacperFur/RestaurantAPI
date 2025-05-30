using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class MenuItem(int id, string name, string description, decimal price, int categoryId, MealType mealType)
    {
        public int Id { get; set; } = id;
        public Guid MenuItemId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = description;
        public decimal Price { get; set; } = price;
        public int? CategoryId { get; set; } = categoryId;
        public string MealType { get; set; } = mealType.ToString();
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
