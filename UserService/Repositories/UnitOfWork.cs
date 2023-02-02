using UserService.Data;

namespace UserService.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public IUserRepository UserRepository => new UserRepository(_context);

        public ICustomerRepository CustomerRepository => new CustomerRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
