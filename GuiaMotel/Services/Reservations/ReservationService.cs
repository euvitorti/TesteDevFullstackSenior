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
            // Busca as entidades de forma segura, prevenindo SQL Injection com EF Core
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == reservationDto.UserId);
            var suiteType = await _context.SuiteTypes.AsNoTracking().FirstOrDefaultAsync(s => s.Id == reservationDto.SuiteTypeId);
            var motel = await _context.Motels.AsNoTracking().FirstOrDefaultAsync(m => m.Id == reservationDto.MotelId);

            if (user == null || suiteType == null || motel == null)
            {
                throw new KeyNotFoundException("Usuário, Suíte ou Motel não encontrado.");
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
            // Uso do AsNoTracking para melhorar a performance em consultas somente leitura
            var reservation = await _context.Reservations.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

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

            // Verifica se os dados já estão em cache
            if (!_cache.TryGetValue(cacheKey, out List<ReservationResponseDTO> cachedReservations))
            {
                // Consulta otimizada utilizando AsNoTracking para melhorar a performance
                var reservations = await _context.Reservations.AsNoTracking()
                    .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
                    .ToListAsync();

                cachedReservations = reservations.Select(r => MapReservationToResponseDTO(r)).ToList();

                // Configuração do cache com expiração deslizante de 5 minutos
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
