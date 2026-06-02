using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Entities
{

        public enum OrderStatus
        {
            Pending,
            Processing,
            Shipped,
            Delivered,
            Cancelled
        }

        public class Order
        {
            public int Id { get; set; }

            [Required]
            public string UserId { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public OrderStatus Status { get; set; } = OrderStatus.Pending;

            public decimal TotalPrice { get; set; }

            public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        }
    }
}
}
