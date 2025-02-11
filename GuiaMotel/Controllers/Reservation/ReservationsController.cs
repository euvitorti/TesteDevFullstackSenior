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
                var reservation = await _reservationService.CreateReservationAsync(reservationDto);

                if (reservation == null)
                    return BadRequest(new { message = "Dados incorretos." });

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

        // Novo endpoint para listar reservas filtradas por data
        // Exemplo: GET /api/reservations/filter?startDate=2025-01-01&endDate=2025-01-31
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ReservationResponseDTO>>> GetReservationsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var reservations = await _reservationService.GetReservationsByDateRangeAsync(startDate, endDate);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
