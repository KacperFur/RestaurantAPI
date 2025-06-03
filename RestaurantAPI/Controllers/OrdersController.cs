using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService service)
        {
            _orderService = service;
        }
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            return Ok(await _orderService.GetAll());
        }

        /// <summary>
        /// Get an order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetById(id);
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
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var orderId = await _orderService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, dto);
        }

        /// <summary>
        /// Update an existing order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> Update(int id, UpdateOrderDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var updated = await _orderService.Update(id, dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var deleted = await _orderService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
