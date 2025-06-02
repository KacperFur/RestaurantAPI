using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Payment
    {
        public int Id { get; set; } 
        public Guid PaymentId { get; set; } 
        public int? OrderId { get; set; } 
        public decimal Amount { get; set; } 
        public string Method { get; set; } 
        public string Status { get; set; } 
        public DateTime? PaidAt { get; set; }
        public Payment() { }
    }
}
