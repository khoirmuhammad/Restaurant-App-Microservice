using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> GetAuth(string username, string password);
        Task Save(User user);
        Task Delete(Guid id);
        Task SoftDelete(Guid id);
    }
}
