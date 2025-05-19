using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private static List<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem(1, "Pizza", "Delicious cheese pizza", 9.99m, "Italian", MealType.MainCourse),
            new MenuItem(2, "Burger", "Juicy beef burger", 5.99m, "American", MealType.MainCourse),
            new MenuItem(3, "Pad Thai", "Fresh intense noodles", 7.99m, "Oriental", MealType.MainCourse)
        };

        /// <summary>
        /// Get all menu items
        /// </summary>
        /// <returns>List of menu items</returns>
        [HttpGet]
        public ActionResult<List<MenuItem>> GetAll()
        {
            return Ok(menuItems);
        }

        /// <summary>
        /// Get a menu item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<MenuItem> GetById(int id)
        {
            var menuItem = menuItems.FirstOrDefault(e => e.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }

        /// <summary>
        /// Create a new menu item
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MenuItem> Create(MenuItem menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest();
            }

            menuItems.Add(menuItem);
            return CreatedAtAction(nameof(GetById), new { id = menuItem.Id }, menuItem);
        }

        /// <summary>
        /// Update an existing menu item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<MenuItem> Update(int id, MenuItem menuItem)
        {
            if (menuItem == null || menuItem.Id != id)
            {
                return BadRequest();
            }
            var existingMenuItem = menuItems.FirstOrDefault(e => e.Id == id);
            if (existingMenuItem == null)
            {
                return NotFound();
            }

            existingMenuItem.Name = menuItem.Name;
            existingMenuItem.Description = menuItem.Description;
            existingMenuItem.Price = menuItem.Price;
            existingMenuItem.Category = menuItem.Category;
            existingMenuItem.MealType = menuItem.MealType;
            return Ok(existingMenuItem);
        }

        /// <summary>
        /// Delete a menu item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var menuItem = menuItems.FirstOrDefault(e => e.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            menuItems.Remove(menuItem);
            return NoContent();
        }
    }
}
