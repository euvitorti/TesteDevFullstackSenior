using DTOs.Reservation;
using Enums;
using GuiaMotel.Data;
using Models.Booking;

namespace Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationResponseDTO> CreateReservationAsync(ReservationDTO reservationDto)
        {
            // Buscar as entidades relacionadas...
            var user = await _context.Users.FindAsync(reservationDto.UserId);
            var suiteType = await _context.SuiteTypes.FindAsync(reservationDto.SuiteTypeId);
            var motel = await _context.Motels.FindAsync(reservationDto.MotelId);

            if (user == null || suiteType == null || motel == null)
            {
                throw new KeyNotFoundException("User, Suite ou Motel não encontrado.");
            }

            // Converte as datas para UTC (se já não estiverem definidas como tal)
            var startDateUtc = DateTime.SpecifyKind(reservationDto.StartDate, DateTimeKind.Utc);
            var endDateUtc = DateTime.SpecifyKind(reservationDto.EndDate, DateTimeKind.Utc);

            var reservation = new Reservation
            {
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                TotalAmount = reservationDto.TotalAmount,
                UserId = reservationDto.UserId,
                SuiteTypeId = reservationDto.SuiteTypeId,
                MotelId = reservationDto.MotelId,
                Status = ReservationStatus.Pending
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return new ReservationResponseDTO
            {
                Id = reservation.Id,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                TotalAmount = reservation.TotalAmount,
                UserId = reservation.UserId,
                SuiteTypeId = reservation.SuiteTypeId,
                MotelId = reservation.MotelId,
                Status = reservation.Status
            };
        }

        public async Task<ReservationResponseDTO> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }

            return new ReservationResponseDTO
            {
                Id = reservation.Id,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                TotalAmount = reservation.TotalAmount,
                UserId = reservation.UserId,
                SuiteTypeId = reservation.SuiteTypeId,
                MotelId = reservation.MotelId,
                Status = reservation.Status
            };
        }
    }
}
