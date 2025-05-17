namespace RestaurantAPI.Entities
{
    public class Table(int id, int tableNumber, int seats, TableStatus status)
    {
        public int Id { get; set; } = id;
        public int TableNumber { get; set; } = tableNumber;
        public int Seats { get; set; } = seats;
        public TableStatus Status { get; set; } = status;
    }
    public enum TableStatus
    {
        Free,
        Occupied,
        Reserved
    }
}