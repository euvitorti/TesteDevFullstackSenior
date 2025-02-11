using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Billing;

namespace Controller.Billing
{
    [ApiController]
    [Route("api/faturamento")]
    [Authorize]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        // GET api/billing/faturamento?referenceDate=2025-01-01T00:00:00Z
        [HttpGet("mes")]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery] DateTime? referenceDate)
        {
            // If no reference date is provided, use the current UTC date
            var date = referenceDate ?? DateTime.UtcNow;
            var revenue = await _billingService.GetMonthlyRevenueAsync(date);
            return Ok(new { MonthlyRevenue = revenue });
        }
    }
}
