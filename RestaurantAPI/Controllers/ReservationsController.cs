using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
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
        public async Task<ActionResult<List<ReservationDto>>> GetAll()
        {
            return Ok(await _reservationService.GetAll());
        }

        /// <summary>
        /// Get a reservation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var reservation = await _reservationService.GetById(id);
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
        public async Task<ActionResult<Reservation>> Create(CreateReservationDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            int newId = await _reservationService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        /// <summary>
        /// Update an existing reservation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task <ActionResult<ReservationDto>> Update(int id, UpdateReservationDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var updatedReservation = await _reservationService.Update(id, dto);
            if (!updatedReservation)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _reservationService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
