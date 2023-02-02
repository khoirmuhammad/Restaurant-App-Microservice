using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;

namespace UserService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var data = await GetById(id);
            if (data != null)
            {
                _context.Customers.Remove(data);
            }
            
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.Include(i => i.User).AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Customer?> GetByUserId(Guid userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task Save(Customer customer)
        {
            var data = GetById(customer.Id);

            if (data == null)
            {
                await _context.Customers.AddAsync(customer);
            }
            else
            {
                _context.Customers.Update(customer);
            }
        }

        public async Task SoftDelete(int id)
        {
            var data = await this.GetById(id);

            if (data != null)
            {
                data.Deleted = true;
            }
        }

        public async Task SoftDelete(Guid userId)
        {
            var data = await this.GetByUserId(userId);

            if (data != null)
            {
                data.Deleted = true;
            }
        }
    }
}
