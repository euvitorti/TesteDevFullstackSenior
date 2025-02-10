using System.ComponentModel.DataAnnotations;

namespace DTOs.SuiteType
{
    public class SuiteTypeDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int MotelId { get; set; }
    }
}