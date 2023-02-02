using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task Delete(Guid id)
        {
            var data = await this.GetById(id);

            if (data != null)
            {
                _context.Users.Remove(data);
            }
            
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetAuth(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(f => f.Username.Equals(username) && f.Password.Equals(password));
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task Save(User user)
        {
            var data = await this.GetById(user.Id);

            if (data == null)
            {
                await _context.Users.AddAsync(user);
            }
            else
            {
                _context.Users.Update(user);
            }
        }

        public async Task SoftDelete(Guid id)
        {
            var data = await this.GetById(id);

            if (data != null)
            {
                data.Deleted = true;
            }
        }
    }
}
