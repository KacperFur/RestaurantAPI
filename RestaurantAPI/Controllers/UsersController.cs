using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrManager")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrManager")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user =  await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize(Roles = "AdminOnly")]
        [HttpPost]
        public ActionResult<int> Create(CreateUserDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var newId = _userService.Create(dto);
            return CreatedAtAction(nameof(GetById),new { Id = newId }, newId);
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, UpdateUserDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var updated = await _userService.Update(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _userService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
