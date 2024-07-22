﻿using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductWebAPI.Models
{
   [Table("Products")]
   public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; } = String.Empty;
        [Required]
        [MaxLength(120)]
        public string Description { get; set; } = String.Empty;
        [Required]
        [Range(1, 50000)]
        public decimal Price { get; set; }
        public List<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
   }
}
