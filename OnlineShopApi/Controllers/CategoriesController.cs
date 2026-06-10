using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            var dtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _categoryService.GetByIdAsync(id);
            if (c == null) return NotFound();
            return Ok(new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var category = new Category { Name = model.Name, Description = model.Description };
            var created = await _categoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var category = new Category { Name = model.Name, Description = model.Description };
            var updated = await _categoryService.UpdateAsync(id, category);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}