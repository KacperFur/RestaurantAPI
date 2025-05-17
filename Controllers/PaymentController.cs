using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private static List<Payment> payments = new List<Payment>
       {
           new Payment(1, 1, 19.99m, PaymentMethod.Card, PaymentStatus.Paid, DateTime.Now),
           new Payment(2, 2, 15.99m, PaymentMethod.Cash, PaymentStatus.Cancelled, DateTime.Now),
           new Payment(3, 3, 12.99m, PaymentMethod.Blik, PaymentStatus.Paid, DateTime.Now)
       };
        [HttpGet]
        public ActionResult<List<Payment>> GetAll()
        {
            return Ok(payments);
        }
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
