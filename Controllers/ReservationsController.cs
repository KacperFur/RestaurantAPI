using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private static List<Reservation> reservations = new List<Reservation>
        { 
            new Reservation(1, 1,1, DateTime.Now, 2, ReservationStatus.Confirmed),
            new Reservation(2,2, 2, DateTime.Now.AddDays(1), 4, ReservationStatus.Cancelled),
            new Reservation(3,2, 3, DateTime.Now.AddDays(2), 6, ReservationStatus.Completed)
        };

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Reservation>> GetAll()
        {
            return Ok(reservations);
        }

        /// <summary>
        /// Get a reservation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetById(int id)
        {
            var reservation = reservations.FirstOrDefault(e => e.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        /// <summary>
        /// Create a new reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Reservation> Create(Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

            reservations.Add(reservation);
            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        /// <summary>
        /// Update an existing reservation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<Reservation> Update(int id, Reservation reservation)
        {
            if (reservation == null || reservation.Id != id)
            {
                return BadRequest();
            }

            var existingReservation = reservations.FirstOrDefault(e => e.Id == id);
            if (existingReservation == null)
            {
                return NotFound();
            }

            existingReservation.UserId = reservation.UserId;
            existingReservation.TableId = reservation.TableId;
            existingReservation.ReservationTime = reservation.ReservationTime;
            existingReservation.GuestCount = reservation.GuestCount;
            existingReservation.Status = reservation.Status;

            return Ok(existingReservation);
        }

        /// <summary>
        /// Delete a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var reservation = reservations.FirstOrDefault(e => e.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservations.Remove(reservation);
            return NoContent();
        }
    }
}
