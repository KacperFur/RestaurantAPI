using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private List<Order> orders = new List<Order>
        {
            new Order(1, 1, 1, DateTime.Now, OrderStatus.Pending, 19.99m, new List<OrderItem>
            {
                new OrderItem(1, 1, 1, 2, 19.99m),
                new OrderItem(2, 1, 2, 1, 5.99m)
            }),
            new Order(2, 2, 2, DateTime.Now, OrderStatus.Completed, 15.99m, new List<OrderItem>
            {
                new OrderItem(3, 2, 3, 1, 7.99m),
                new OrderItem(4, 2, 4, 1, 8.00m)
            }),
            new Order(3, 3, 3, DateTime.Now, OrderStatus.Cancelled, 12.99m, new List<OrderItem>
            {
                new OrderItem(5, 3, 5, 1, 12.99m)
            })
        };

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Order>> GetAll()
        {
            return Ok(orders);
        }

        /// <summary>
        /// Get an order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = orders.FirstOrDefault(e => e.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            orders.Add(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Update an existing order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<Order> Update(int id, Order order)
        {
            if (order == null || order.Id != id)
            {
                return BadRequest();
            }

            var existingOrder = orders.FirstOrDefault(e => e.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.UserId = order.UserId;
            existingOrder.DishId = order.DishId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.OrderItems = order.OrderItems;
            return Ok(existingOrder);
        }

        /// <summary>
        /// Delete an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var order = orders.FirstOrDefault(e => e.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            orders.Remove(order);
            return NoContent();
        }
    }
}
