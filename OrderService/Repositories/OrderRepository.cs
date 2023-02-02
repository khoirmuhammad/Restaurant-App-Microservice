using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _contex;

        public OrderRepository(ApplicationContext context)
        {
            _contex = context;
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _contex.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetOrderById(Guid id)
        {
            return await _contex.Orders.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrderHistoryByUser(Guid userId)
        {
            return await _contex.Orders.Where(w => w.UserId.Equals(userId)).AsNoTracking().ToListAsync();
        }

        public async Task Save(Order order)
        {
            var data = await GetOrderById(order.Id);

            if (data == null)
            {
                await _contex.Orders.AddAsync(order);
            }
            else
            {
                _contex.Orders.Update(data);
            }
        }

        public async Task UpdateStatus(Order order)
        {
            var data = await GetOrderById(order.Id);

            if (data != null)
            {
                data.Status = order.Status;
            }
        }
    }
}
