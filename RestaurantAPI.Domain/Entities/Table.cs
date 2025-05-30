using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Table(int id, int tableNumber, int seats, TableStatus status)
    {
        public int Id { get; set; } = id;
        public Guid TableId { get; set; } = Guid.NewGuid();
        public int TableNumber { get; set; } = tableNumber;
        public int Seats { get; set; } = seats;
        public string Status { get; set; } = status.ToString();
    }
}