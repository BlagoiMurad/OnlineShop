using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var dtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _productService.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            });
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            var dtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            });
            return Ok(dtos);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var products = await _productService.GetAllAsync();
            var filtered = products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    ImageUrl = p.ImageUrl,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category?.Name ?? string.Empty
                });
            return Ok(filtered);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId
            };
            var created = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId
            };
            var updated = await _productService.UpdateAsync(id, product);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}