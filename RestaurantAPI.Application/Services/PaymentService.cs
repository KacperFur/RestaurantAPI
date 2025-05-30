using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private static List<Payment> payments = new List<Payment>
       {
           new Payment(1, 1, 19.99m, PaymentMethod.Card, PaymentStatus.Paid, DateTime.Now),
           new Payment(2, 2, 15.99m, PaymentMethod.Cash, PaymentStatus.Cancelled, DateTime.Now),
           new Payment(3, 3, 12.99m, PaymentMethod.Blik, PaymentStatus.Paid, DateTime.Now)
       };

        public void Create(Payment payment)
        {
            payments.Add(payment);
        }

        public bool Delete(int id)
        {
            var payment = payments.FirstOrDefault(e => e.Id == id);
            if (payment == null)
            {
                return false;
            }

            payments.Remove(payment);
            return true;
        }

        public List<Payment> GetAll()
        {
            return payments;
        }

        public Payment GetById(int id)
        {
            return payments.FirstOrDefault(e => e.Id == id);
        }

        public bool Update(int id, Payment payment)
        {
            var existingPayment = payments.FirstOrDefault(e => e.Id == id);
            if (existingPayment == null)
            {
                return false;
            }
            existingPayment.OrderId = payment.OrderId;
            existingPayment.Amount = payment.Amount;
            existingPayment.Method = payment.Method;
            existingPayment.Status = payment.Status;
            existingPayment.PaidAt = payment.PaidAt;
            return true;
        }
    }
}
