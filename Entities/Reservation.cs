namespace RestaurantAPI.Entities
{
    public class Reservation(int id, int tableId,int userId ,DateTime reservationTime, int guestCount, ReservationStatus status)
    {
        public int Id { get; set; } = id;
        public int TableId { get; set; } = tableId;
        public Table Table { get; set; }
        public int UserId { get; set; } = userId;
        public User User { get; set; }
        public DateTime ReservationTime { get; set; } = reservationTime;
        public int GuestCount { get; set; } = guestCount;
        public ReservationStatus Status { get; set; } = status;
    }
    public enum ReservationStatus
    {
        Confirmed,
        Cancelled,
        Completed
    }
}