using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Models
{
    public class UpdateTableDto
    {
        public int TableNumber { get; set; }    
        public int Seats { get; set; }          
        public string Status { get; set; }      
    }
}
