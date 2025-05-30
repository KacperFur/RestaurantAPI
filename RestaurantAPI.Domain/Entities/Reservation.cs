using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Reservation(int id, int tableId, int userId, DateTime reservationTime, int guestCount, ReservationStatus status)
    {
        public int Id { get; set; } = id;
        public Guid ReservationId { get; set; } = Guid.NewGuid();
        public int? TableId { get; set; } = tableId;
        public int? UserId { get; set; } = userId;
        public DateTime ReservationTime { get; set; } = reservationTime;    
        public int GuestCount { get; set; } = guestCount;
        public string Status { get; set; } = status.ToString();
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}