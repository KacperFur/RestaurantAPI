namespace RestaurantAPI.Application.Models
{
    public class CreatePaymentDto
    {
        public int? OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
