using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Services;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationsController(IReservationService service)
        {
            _reservationService = service;   
        }
        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Reservation>> GetAll()
        {
            return Ok(_reservationService.GetAll());
        }

        /// <summary>
        /// Get a reservation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetById(int id)
        {
            var reservation = _reservationService.GetById(id);
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

            _reservationService.Create(reservation);
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

            var updatedReservation = _reservationService.Update(id,reservation);
            if (!updatedReservation)
            {
                return NotFound();
            }
            return Ok(updatedReservation);
        }

        /// <summary>
        /// Delete a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleted = _reservationService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
