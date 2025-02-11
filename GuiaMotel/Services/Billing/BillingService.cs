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

        /// <summary>
        /// Calcula a receita mensal somando o valor total (TotalAmount) de todas as reservas
        /// que iniciam no período especificado pelo primeiro dia do mês até o primeiro dia do mês seguinte.
        /// </summary>
        /// <param name="referenceDate">Data de referência para determinar o mês</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional)</param>
        /// <returns>Receita total do mês</returns>
        public async Task<decimal> GetMonthlyRevenueAsync(DateTime referenceDate, CancellationToken cancellationToken = default)
        {
            // Garantir que a data seja tratada como UTC e obter o primeiro dia do mês
            var startDate = new DateTime(referenceDate.Year, referenceDate.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            // Data final não inclusiva: primeiro dia do próximo mês
            var endDate = startDate.AddMonths(1);

            // A consulta abaixo utiliza o Entity Framework, que por padrão utiliza parâmetros,
            // garantindo proteção contra SQL Injection.
            // A query filtra as reservas com StartDate entre startDate (inclusive) e endDate (exclusive)
            // e realiza a soma do campo TotalAmount.
            var revenue = await _context.Reservations
                .Where(r => r.StartDate >= startDate && r.StartDate < endDate)
                .SumAsync(r => r.TotalAmount, cancellationToken);

            return revenue;
        }
    }
}
