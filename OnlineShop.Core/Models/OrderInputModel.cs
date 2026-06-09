using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Models
{
    public class OrderInputModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public List<OrderItemInputModel> OrderItems { get; set; } = new();
    }

    public class OrderItemInputModel
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.01, 99999.99)]
        public decimal Price { get; set; }
    }
}