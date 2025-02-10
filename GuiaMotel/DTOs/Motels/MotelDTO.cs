using System.ComponentModel.DataAnnotations;

namespace DTOs.Motels
{
    public class MotelDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string CNPJ { get; set; } = string.Empty;
    }
}