using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guid ReservationId { get; set; } 
        public int? TableId { get; set; } 
        public int? UserId { get; set; } 
        public DateTime ReservationTime { get; set; }     
        public int GuestCount { get; set; }
        public string Status { get; set; } 
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Reservation() { } 
    }
}