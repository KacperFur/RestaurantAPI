using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService service)
        {
            _paymentService = service;
        }
        /// <summary>
        /// Get all payments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PaymentDto>>> GetAll()
        {
            return Ok(await _paymentService.GetAll());
        }

        /// <summary>
        /// Get a payment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetById(int id)
        {
            var payment = await _paymentService.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        /// <summary>
        /// Create a new payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> Create(CreatePaymentDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var newId = await _paymentService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        /// <summary>
        /// Update an existing payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDto>> Update(int id, UpdatePaymentDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var updated = await _paymentService.Update(id, dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _paymentService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
