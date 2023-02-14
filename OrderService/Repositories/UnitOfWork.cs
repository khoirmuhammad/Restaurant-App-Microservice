using OrderService.Data;

namespace OrderService.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public IOrderRepository OrderRepository => new OrderRepository(_context);
        public IOrderDetailRepository OrderDetailRepository => new OrderDetailRepository(_context);
        public IFoodViewModelRepository FoodViewModelRepository => new FoodViewModelRepository(_context);

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
