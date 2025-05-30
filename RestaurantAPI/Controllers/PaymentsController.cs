using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Entities;

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
        public ActionResult<List<Payment>> GetAll()
        {
            return Ok(_paymentService.GetAll());
        }

        /// <summary>
        /// Get a payment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Payment> GetById(int id)
        {
            var payment = _paymentService.GetById(id);
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
        public ActionResult<Payment> Create(Payment payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            _paymentService.Create(payment);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }

        /// <summary>
        /// Update an existing payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<Payment> Update(int id, Payment payment)
        {
            if (payment == null || payment.Id != id)
            {
                return BadRequest();
            }

            var updated = _paymentService.Update(id, payment);
            if (updated)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        /// <summary>
        /// Delete a payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleted = _paymentService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
