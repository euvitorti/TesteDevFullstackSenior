using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;
using GuiaMotel.Model;
using Models.Models;
using Suite;

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
        public User? User { get; set; }  // Propriedade do tipo User, agora pode ser null

        // Relacionamento com o SuiteType
        public int SuiteTypeId { get; set; }

        [ForeignKey("SuiteTypeId")]
        public SuiteType? SuiteType { get; set; }  // Propriedade do tipo SuiteType, agora pode ser null

        // Relacionamento com o Motel
        public int MotelId { get; set; }

        [ForeignKey("MotelId")]
        public Motel? Motel { get; set; }  // Propriedade do tipo Motel, agora pode ser null

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    }
}
