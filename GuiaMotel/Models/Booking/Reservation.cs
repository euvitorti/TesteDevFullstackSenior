using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;
using GuiaMotel.Model;
using Models.Motels;
using Models.SuiteType;

namespace Models.Booking
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime StartDate { get; set; } // Data de início da reserva
        public DateTime EndDate { get; set; } // Data de término da reserva
        public decimal TotalAmount { get; set; } // Valor total da reserva

        // Relacionamento com o User (Cliente)
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        // Relacionamento com o SuiteType
        public int SuiteTypeId { get; set; }

        [ForeignKey("SuiteTypeId")]
        public Suite? SuiteType { get; set; }

        // Relacionamento com o Motel
        public int MotelId { get; set; }

        [ForeignKey("MotelId")]
        public Motel? Motel { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    }
}
