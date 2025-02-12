using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Booking;
using Models.SuiteType;

namespace Models.Motels
{
    public class Motel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty; // Cadastro Nacional de Pessoa Jurídica

        // Relacionamento com Tipos de Suíte
        public ICollection<Suite> SuiteTypes { get; set; } = new List<Suite>();

        // Relacionamento com Reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
