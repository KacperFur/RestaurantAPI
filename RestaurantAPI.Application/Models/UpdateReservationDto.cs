namespace RestaurantAPI.Application.Models
{
    public class UpdateReservationDto
    {
        public int? TableId { get; set; }              
        public int? UserId { get; set; }               
        public DateTime ReservationTime { get; set; }  
        public int GuestCount { get; set; }            
        public string Status { get; set; }             
    }
}
