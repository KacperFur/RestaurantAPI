using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        public MenuItemsController(IMenuItemService service)
        {
            _menuItemService = service;
        }
        /// <summary>
        /// Get all menu items
        /// </summary>
        /// <returns>List of menu items</returns>
        [HttpGet]
        public ActionResult<List<MenuItem>> GetAll()
        {
            return Ok(_menuItemService.GetAll());
        }

        /// <summary>
        /// Get a menu item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<MenuItem> GetById(int id)
        {
            var menuItem = _menuItemService.GetById(id);
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

            _menuItemService.Create(menuItem);
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

            var updated = _menuItemService.Update(id, menuItem);
            if (updated)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        /// <summary>
        /// Delete a menu item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleted = _menuItemService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
