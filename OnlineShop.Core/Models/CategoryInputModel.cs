using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Models
{
    public class CategoryInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}