using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var dtos = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString(),
                TotalPrice = o.TotalPrice,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? string.Empty,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var o = await _orderService.GetByIdAsync(id);
            if (o == null) return NotFound();
            return Ok(new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString(),
                TotalPrice = o.TotalPrice,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? string.Empty,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var orders = await _orderService.GetByUserIdAsync(userId);
            var dtos = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString(),
                TotalPrice = o.TotalPrice
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var order = new Order
            {
                UserId = model.UserId,
                OrderItems = model.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
            var created = await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderStatus status)
        {
            var updated = await _orderService.UpdateStatusAsync(id, status);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}