using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private static List<Payment> payments = new List<Payment>
       {
           new Payment(1, 1, 19.99m, PaymentMethod.Card, PaymentStatus.Paid, DateTime.Now),
           new Payment(2, 2, 15.99m, PaymentMethod.Cash, PaymentStatus.Cancelled, DateTime.Now),
           new Payment(3, 3, 12.99m, PaymentMethod.Blik, PaymentStatus.Paid, DateTime.Now)
       };

        /// <summary>
        /// Get all payments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Payment>> GetAll()
        {
            return Ok(payments);
        }

        /// <summary>
        /// Get a payment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Payment> GetById(int id)
        {
            var payment = payments.FirstOrDefault(e => e.Id == id);
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

            payments.Add(payment);
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

            var existingPayment = payments.FirstOrDefault(e => e.Id == id);
            if (existingPayment == null)
            {
                return NotFound();
            }

            existingPayment.OrderId = payment.OrderId;
            existingPayment.Amount = payment.Amount;
            existingPayment.Method = payment.Method;
            existingPayment.Status = payment.Status;
            existingPayment.PaidAt = payment.PaidAt;
            return Ok(existingPayment);
        }

        /// <summary>
        /// Delete a payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var payment = payments.FirstOrDefault(e => e.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            payments.Remove(payment);
            return NoContent();
        }
    }
}
