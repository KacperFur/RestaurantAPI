using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        /// <summary>
        /// Get all tables
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TableDto>>> GetAll()
        {
            return Ok(await _tableService.GetAll());
        }

        /// <summary>
        /// Get a table by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TableDto>> GetById(int id)
        {
            var table = await _tableService.GetById(id);
            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        /// <summary>
        /// Create a new table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TableDto>> Create(CreateTableDto table)
        {
            if (table == null)
            {
                return BadRequest();
            }
            
            int newId = await _tableService.Create(table);
            return CreatedAtAction(nameof(GetById), new { id = newId}, table);
        }

        /// <summary>
        /// Update an existing table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TableDto>> Update(int id, UpdateTableDto table)
        {
            if (table == null)
            {
                return BadRequest();
            }
            var updated = await _tableService.Update(id, table);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete a table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _tableService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
