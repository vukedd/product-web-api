using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Name { get; set; } = String.Empty;
        [Required]
        [MaxLength(120)]
        public string Description { get; set; } = String.Empty;
        [Required]
        [Range(1, 50000)]
        public decimal Price { get; set; }
    }
}
