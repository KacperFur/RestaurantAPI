using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public Guid TableId { get; set; }
        public int TableNumber { get; set; }
        public int Seats { get; set; } 
        public string Status { get; set; }
        public Table() { }
    }
}