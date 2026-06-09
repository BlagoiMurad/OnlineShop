using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
        Task<Order> CreateAsync(Order order);
        Task<Order?> UpdateStatusAsync(int id, OrderStatus status);
        Task<bool> DeleteAsync(int id);
    }
}