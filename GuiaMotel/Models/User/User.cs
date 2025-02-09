using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.Booking;

namespace GuiaMotel.Model
{
    [Index(nameof(Email), IsUnique = true)] // Índice único para Email
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Relacionamento com as Reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
