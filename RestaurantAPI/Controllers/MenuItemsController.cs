using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
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
        public async Task<ActionResult<List<MenuItemDto>>> GetAll()
        {
            return Ok(await _menuItemService.GetAll());
        }

        /// <summary>
        /// Get a menu item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id)
        {
            var menuItem = await _menuItemService.GetById(id);
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
        public async Task<ActionResult<MenuItemDto>> Create(CreateMenuItemDto menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest();
            }

            var newId = await _menuItemService.Create(menuItem);
            return CreatedAtAction(nameof(GetById), new { id = newId } , menuItem);
        }

        /// <summary>
        /// Update an existing menu item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItemDto>> Update(int id, UpdateMenuItemDto menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest();
            }

            var updated = await _menuItemService.Update(id, menuItem);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a menu item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _menuItemService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
