using Microsoft.AspNetCore.Mvc;
using DTOs.Reservation;
using Services.Reservations;
using Microsoft.AspNetCore.Authorization;

namespace Controller.Reservation
{
    [ApiController]
    [Route("api/reservations")]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // Endpoint para criar a reserva
        [HttpPost]
        public async Task<ActionResult<ReservationResponseDTO>> CreateReservation([FromBody] ReservationDTO reservationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Chama o serviço para criar a reserva
                var reservation = await _reservationService.CreateReservationAsync(reservationDto);

                if (reservation == null)
                    return BadRequest(new { message = "Dados incorretos." });

                // Retorna a reserva criada com status 201 (Created)
                return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Endpoint para buscar uma reserva pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationResponseDTO>> GetReservationById(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                return Ok(reservation);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
