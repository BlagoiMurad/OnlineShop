using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineShop.Core.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Services;

namespace OnlineShop.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private ApplicationDbContext _context;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _orderService = new OrderService(_context);

            _context.Orders.AddRange(
                new Order { Id = 1, UserId = "user1", Status = OrderStatus.Pending, TotalPrice = 999.99m },
                new Order { Id = 2, UserId = "user2", Status = OrderStatus.Shipped, TotalPrice = 499.99m }
            );
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllOrders()
        {
            var result = await _orderService.GetAllAsync();
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_ExistingId_ReturnsOrder()
        {
            var result = await _orderService.GetByIdAsync(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.UserId, Is.EqualTo("user1"));
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            var result = await _orderService.GetByIdAsync(99);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetByUserIdAsync_ReturnsOrdersForUser()
        {
            var result = await _orderService.GetByUserIdAsync("user1");
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task CreateAsync_ValidOrder_ReturnsCreatedOrder()
        {
            var order = new Order
            {
                UserId = "user3",
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2, Price = 100m }
                }
            };
            var result = await _orderService.CreateAsync(order);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo("user3"));
        }

        [Test]
        public async Task UpdateStatusAsync_ExistingOrder_ReturnsUpdatedOrder()
        {
            var result = await _orderService.UpdateStatusAsync(1, OrderStatus.Delivered);
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Status, Is.EqualTo(OrderStatus.Delivered));
        }

        [Test]
        public async Task DeleteAsync_ExistingOrder_ReturnsTrue()
        {
            var result = await _orderService.DeleteAsync(1);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteAsync_NonExistingOrder_ReturnsFalse()
        {
            var result = await _orderService.DeleteAsync(99);
            Assert.That(result, Is.False);
        }
    }
}