using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using OnlineShop.Core.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Services;

namespace OnlineShop.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ApplicationDbContext _context;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _productService = new ProductService(_context);

            var category = new Category { Id = 1, Name = "Electronics", Description = "Test" };
            _context.Categories.Add(category);

            _context.Products.AddRange(
                new Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "Phone", Price = 499.99m, Stock = 20, CategoryId = 1 }
            );
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            var result = await _productService.GetAllAsync();
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_ExistingId_ReturnsProduct()
        {
            var result = await _productService.GetByIdAsync(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Laptop"));
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            var result = await _productService.GetByIdAsync(99);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_ValidProduct_ReturnsCreatedProduct()
        {
            var product = new Product { Name = "Tablet", Price = 299.99m, Stock = 5, CategoryId = 1 };
            var result = await _productService.CreateAsync(product);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Tablet"));
        }

        [Test]
        public async Task UpdateAsync_ExistingProduct_ReturnsUpdatedProduct()
        {
            var updated = new Product { Name = "Updated Laptop", Price = 1099.99m, Stock = 8, CategoryId = 1 };
            var result = await _productService.UpdateAsync(1, updated);
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Updated Laptop"));
        }

        [Test]
        public async Task DeleteAsync_ExistingProduct_ReturnsTrue()
        {
            var result = await _productService.DeleteAsync(1);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteAsync_NonExistingProduct_ReturnsFalse()
        {
            var result = await _productService.DeleteAsync(99);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetByCategoryAsync_ReturnsProductsInCategory()
        {
            var result = await _productService.GetByCategoryAsync(1);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}