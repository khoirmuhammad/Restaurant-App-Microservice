using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetOrderById(Guid id)
        {
            return await _context.Orders.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrderHistoryByUser(Guid userId)
        {
            return await _context.Orders.Where(w => w.UserId.Equals(userId)).AsNoTracking().ToListAsync();
        }

        public async Task Save(Order order)
        {
            var data = await GetOrderById(order.Id);

            if (data == null)
            {
                await _context.Orders.AddAsync(order);
            }
            else
            {
                _context.Orders.Update(data);
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
