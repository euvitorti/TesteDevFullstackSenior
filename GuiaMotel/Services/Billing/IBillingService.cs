namespace Services.Billing
{
    public interface IBillingService
    {
        /// <summary>
        /// Calcula a receita total (soma do TotalAmount) para as reservas dentro do mês especificado.
        /// </summary>
        /// <param name="referenceDate">
        /// Qualquer data dentro do mês para o qual a receita deve ser calculada.
        /// </param>
        /// <param name="cancellationToken">
        /// Token para cancelamento da operação assíncrona (opcional).
        /// </param>
        /// <returns>Receita total para o mês.</returns>
        Task<decimal> GetMonthlyRevenueAsync(DateTime referenceDate, CancellationToken cancellationToken = default);
    }
}
