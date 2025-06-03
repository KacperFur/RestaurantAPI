namespace RestaurantAPI.Application.Models
{
    public class TableDto
    {
        public int Id { get; set; }
        public Guid TableId { get; set; }
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public string Status { get; set; }
    }
}
