using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Interfaces
{
    public interface IReservationService
    {
        List<Reservation> GetAll();
        Reservation GetById(int id);
        void Create(Reservation reservation);
        bool Update(int id, Reservation reservation);
        bool Delete(int id);
    }
}
