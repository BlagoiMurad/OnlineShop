using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Models
{
    public class ProductInputModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 99999.99)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
    }
}