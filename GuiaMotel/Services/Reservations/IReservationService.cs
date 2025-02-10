using DTOs.Reservation;

namespace Services.Reservations
{
    public interface IReservationService
    {
        Task<ReservationResponseDTO> CreateReservationAsync(ReservationDTO reservationDto);
        Task<ReservationResponseDTO> GetReservationByIdAsync(int id);
    }
}
