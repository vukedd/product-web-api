using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.DTO
{
    public class ProductDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
        [Required]
        [Range(1, 50000)]
        public decimal Price { get; set; }
    }
}
