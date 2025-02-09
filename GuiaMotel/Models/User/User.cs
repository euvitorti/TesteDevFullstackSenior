using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GuiaMotel.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key] // Indica explicitamente que essa propriedade é a chave primária.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Faz com que o valor seja gerado automaticamente pelo banco.
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;  // valor padrão
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
