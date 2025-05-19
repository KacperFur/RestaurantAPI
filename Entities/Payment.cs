namespace RestaurantAPI.Entities
{
    public class Payment(int id, int orderId, decimal amount, PaymentMethod method, PaymentStatus status, DateTime paidAt)
    {
        public int Id { get; set; } = id;
        public int OrderId { get; set; } = orderId;
        public decimal Amount { get; set; } = amount;
        public PaymentMethod Method { get; set; } = method;
        public PaymentStatus Status { get; set; } = status;
        public DateTime PaidAt { get; set; } = paidAt;
    }
    public enum PaymentMethod
    {
        Cash,
        Card,
        Blik
    }
    public enum PaymentStatus
    {
        Paid,
        Cancelled
    }
}
