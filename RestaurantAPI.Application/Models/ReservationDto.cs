using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Models
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public Guid ReservationId { get; set; }
        public int? TableId { get; set; }
        public int? UserId { get; set; }
        public DateTime ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public string Status { get; set; }
    }
}
