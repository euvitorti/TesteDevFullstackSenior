namespace Services.Billing
{
    public interface IBillingService
    {
        /// <summary>
        /// Calculates the total revenue (sum of TotalAmount) for reservations within the specified month.
        /// </summary>
        /// <param name="referenceDate">Any date within the month to calculate revenue for. If not provided, the current month is used.</param>
        /// <returns>Total revenue for the month.</returns>
        Task<decimal> GetMonthlyRevenueAsync(DateTime referenceDate);
    }
}
