namespace RestaurantAPI.Application.Models
{
    public class CreateTableDto
    {
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public string Status { get; set; }
    }
}
