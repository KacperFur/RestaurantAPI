using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Services;
using RestaurantAPI.Entities;

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
        public ActionResult<List<Table>> GetAll()
        {
            return Ok(_tableService.GetAll());
        }

        /// <summary>
        /// Get a table by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Table> GetById(int id)
        {
            var table = _tableService.GetById(id);
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
        public ActionResult<Table> Create(Table table)
        {
            if (table == null)
            {
                return BadRequest();
            }
            
            _tableService.Create(table);
            return CreatedAtAction(nameof(GetById), new { id = table.Id }, table);
        }

        /// <summary>
        /// Update an existing table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<Table> Update(int id, Table table)
        {
            if (table == null ||  table.Id != id)
            {
                return BadRequest();
            }
            var updated = _tableService.Update(id, table);
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
        public ActionResult Delete(int id)
        {
            var deleted = _tableService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
