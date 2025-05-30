using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class ReservationService : IReservationService
    {
        private static List<Reservation> reservations = new List<Reservation>
        {
            new Reservation(1, 1,1, DateTime.Now, 2, ReservationStatus.Confirmed),
            new Reservation(2,2, 2, DateTime.Now.AddDays(1), 4, ReservationStatus.Cancelled),
            new Reservation(3,2, 3, DateTime.Now.AddDays(2), 6, ReservationStatus.Completed)
        };

        public void Create(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        public bool Delete(int id)
        {
            var reservation = reservations.FirstOrDefault(e => e.Id == id);
            if (reservation == null)
            {
                return false;
            }

            reservations.Remove(reservation);
            return true;
        }

        public List<Reservation> GetAll()
        {
            return reservations;
        }

        public Reservation GetById(int id)
        { 
            return reservations.FirstOrDefault(e => e.Id == id);
        }

        public bool Update(int id, Reservation reservation)
        {
            var existingReservation = reservations.FirstOrDefault(e => e.Id == id);
            if (existingReservation == null)
            {
                return false;
            }

            existingReservation.UserId = reservation.UserId;
            existingReservation.TableId = reservation.TableId;
            existingReservation.ReservationTime = reservation.ReservationTime;
            existingReservation.GuestCount = reservation.GuestCount;
            existingReservation.Status = reservation.Status;
            return true;
        }
    }
}
