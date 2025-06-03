namespace RestaurantAPI.Application.Models
{
    public class UpdatePaymentDto
    {
        public string Method { get; set; }      
        public string Status { get; set; }   
        public DateTime? PaidAt { get; set; }
    }
}
