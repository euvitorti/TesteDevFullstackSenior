using GuiaMotel.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Billing
{
    public class BillingService : IBillingService
    {
        private readonly ApplicationDbContext _context;

        public BillingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetMonthlyRevenueAsync(DateTime referenceDate)
        {
            // Ensure the date is treated as UTC and get the first day of the month
            var startDate = new DateTime(referenceDate.Year, referenceDate.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1); // non-inclusive end date

            // Sum up the TotalAmount for all reservations that start within the month
            var revenue = await _context.Reservations
                .Where(r => r.StartDate >= startDate && r.StartDate < endDate)
                .SumAsync(r => r.TotalAmount);

            return revenue;
        }
    }
}
