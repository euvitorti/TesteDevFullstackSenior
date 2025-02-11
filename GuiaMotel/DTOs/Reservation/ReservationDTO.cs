using System.ComponentModel.DataAnnotations;

namespace DTOs.Reservation
{
    public class ReservationDTO
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SuiteTypeId { get; set; }

        [Required]
        public int MotelId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
    }
}
