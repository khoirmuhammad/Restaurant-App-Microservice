using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetOrderHistoryByUser(Guid userId);
        Task<Order?> GetOrderById(Guid id);
        Task Save(Order order);
        Task UpdateStatus(Order order);
    }
}
