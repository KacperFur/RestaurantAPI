using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TablesController : ControllerBase
    {
        private static List<Table> tables = new List<Table>
        {
            new Table(1, 4,2, TableStatus.Free),
            new Table(2, 2,3, TableStatus.Occupied),
            new Table(3, 6,4, TableStatus.Reserved)
        };

        /// <summary>
        /// Get all tables
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Table>> GetAll()
        {
            return Ok(tables);
        }

        /// <summary>
        /// Get a table by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Table> GetById(int id)
        {
            var table = tables.FirstOrDefault(e => e.Id == id);
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
            
            tables.Add(table);
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
            if (table == null || table.Id != id)
            {
                return BadRequest();
            }

            var existingTable = tables.FirstOrDefault(e => e.Id == id);
            if (existingTable == null)
            {
                return NotFound();
            }

            existingTable.Seats = table.Seats;
            existingTable.TableNumber = table.TableNumber;
            existingTable.Status = table.Status;
            return Ok(existingTable);
        }

        /// <summary>
        /// Delete a table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var table = tables.FirstOrDefault(e => e.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            tables.Remove(table);
            return NoContent();
        }
    }
}
