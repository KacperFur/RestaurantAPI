using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private static List<Table> tables = new List<Table>
        {
            new Table(1, 4,2, TableStatus.Free),
            new Table(2, 2,3, TableStatus.Occupied),
            new Table(3, 6,4, TableStatus.Reserved)
        };
        [HttpGet]
        public ActionResult<List<Table>> GetAll()
        {
            return Ok(tables);
        }
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
