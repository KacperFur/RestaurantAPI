using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private static List<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem(1, "Pizza", "Delicious cheese pizza", 9.99m, "Italian", MealType.MainCourse),
            new MenuItem(2, "Burger", "Juicy beef burger", 5.99m, "American", MealType.MainCourse),
            new MenuItem(3, "Pad Thai", "Fresh intense noodles", 7.99m, "Oriental", MealType.MainCourse)
        };

        public void Create(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
        }

        public bool Delete(int id)
        {
            var menuItem = menuItems.FirstOrDefault(e => e.Id == id);
            if (menuItem == null)
            {
                return false;
            }

            menuItems.Remove(menuItem);
            return true;
        }

        public List<MenuItem> GetAll()
        {
            return menuItems;
        }

        public MenuItem GetById(int id)
        {
            return menuItems.FirstOrDefault(e => e.Id == id);
        }

        public bool Update(int id, MenuItem menuItem)
        {
            var existingMenuItem = menuItems.FirstOrDefault(e => e.Id == id);
            if (existingMenuItem == null)
            {
                return false;
            }
            existingMenuItem.Name = menuItem.Name;
            existingMenuItem.Description = menuItem.Description;
            existingMenuItem.Price = menuItem.Price;
            existingMenuItem.Category = menuItem.Category;
            existingMenuItem.MealType = menuItem.MealType;
            return true;
        }
    }
}
