using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Payment(int id, int orderId, decimal amount, PaymentMethod method, PaymentStatus status, DateTime paidAt)
    {
        public int Id { get; set; } = id;
        public Guid PaymentId { get; set; } = Guid.NewGuid();
        public int? OrderId { get; set; } = orderId;
        public decimal Amount { get; set; } = amount;
        public string Method { get; set; } = method.ToString();
        public string Status { get; set; } = status.ToString();
        public DateTime? PaidAt { get; set; }
    }
}
