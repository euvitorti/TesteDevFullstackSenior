using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Booking;
using Models.Motels;

namespace Models.SuiteType
{
    public class Suite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // Nome do tipo de suíte
        public decimal Price { get; set; } // Preço da suíte

        // Relacionamento com o Motel
        public int MotelId { get; set; }
        
        [ForeignKey("MotelId")]
        public Motel? Motel { get; set; } // Making Motel nullable

        // Relacionamento com Reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
