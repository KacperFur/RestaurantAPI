namespace RestaurantAPI.Entities
{
    public class MenuItem(int id, string name, string description, decimal price, string category, MealType mealType)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public decimal Price { get; set; } = price;
        public string Category { get; set; } = category;
        public MealType MealType { get; set; } = mealType;
    }

    public enum MealType
    {
        Appetizer,
        MainCourse,
        Dessert,
        Beverage
    }
}
