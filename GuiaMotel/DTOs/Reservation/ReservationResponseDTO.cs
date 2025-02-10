using Enums;

namespace DTOs.Reservation
{
    public class ReservationResponseDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public int SuiteTypeId { get; set; }
        public int MotelId { get; set; }
        public ReservationStatus Status { get; set; }
    }

}