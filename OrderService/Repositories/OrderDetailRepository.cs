using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationContext _context;
        public OrderDetailRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(Guid orderId)
        {
            return await _context.OrdersDetails.Where(w => w.OrderId == orderId).AsNoTracking().ToListAsync();
        }

        public async Task SaveBulk(List<OrderDetail> orderDetailList)
        {
            await _context.AddRangeAsync(orderDetailList);
        }
    }
}
