using DTOs.Reservation;
using Enums;
using GuiaMotel.Data;
using Infra.Extensions; // Namespace da classe DateTimeExtensions
using Models.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory; // Importação do MemoryCache

namespace Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache; // Declaração do cache

        public ReservationService(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<ReservationResponseDTO> CreateReservationAsync(ReservationDTO reservationDto)
        {
            var user = await _context.Users.FindAsync(reservationDto.UserId);
            var suiteType = await _context.SuiteTypes.FindAsync(reservationDto.SuiteTypeId);
            var motel = await _context.Motels.FindAsync(reservationDto.MotelId);

            if (user == null || suiteType == null || motel == null)
            {
                throw new KeyNotFoundException("User, Suite ou Motel não encontrado.");
            }

            // Utilize o método de extensão para converter para UTC
            var startDateUtc = reservationDto.StartDate.ToUtc();
            var endDateUtc = reservationDto.EndDate.ToUtc();

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

            // Invalida o cache após criar uma nova reserva
            _cache.Remove("all_reservations");

            return MapReservationToResponseDTO(reservation);
        }

        public async Task<ReservationResponseDTO> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }

            return MapReservationToResponseDTO(reservation);
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Converte as datas para UTC usando o método de extensão, garantindo que os comparativos estejam corretos
            startDate = startDate.ToUtc();
            endDate = endDate.ToUtc();

            string cacheKey = $"reservations_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

            if (!_cache.TryGetValue(cacheKey, out List<ReservationResponseDTO> cachedReservations))
            {
                var reservations = await _context.Reservations
                    .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
                    .ToListAsync();

                cachedReservations = reservations.Select(r => MapReservationToResponseDTO(r)).ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, cachedReservations, cacheOptions);
            }

            return cachedReservations;
        }

        // Método privado para mapear uma entidade Reservation para ReservationResponseDTO
        private ReservationResponseDTO MapReservationToResponseDTO(Reservation reservation)
        {
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
