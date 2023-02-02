using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(Guid orderId);
        Task SaveBulk(List<OrderDetail> orderDetailList);
    }
}
